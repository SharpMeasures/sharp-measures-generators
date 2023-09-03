namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="IVectorQuantityRecorderFactory"/>
public sealed class VectorQuantityRecorderFactory : IVectorQuantityRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IVectorQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="VectorQuantityRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorQuantityAttribute{TUnit}"/> to recorders.</param>
    public VectorQuantityRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IVectorQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IVectorQuantityRecord> IVectorQuantityRecorderFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new VectorQuantityRecordBuilder(attributeSyntax);

        return Factory.Create<IVectorQuantityRecord, IVectorQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorQuantityRecordBuilder : ARecordBuilder<IVectorQuantityRecord>, IVectorQuantityRecordBuilder
    {
        private VectorQuantityRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public VectorQuantityRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticVectorQuantityRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IVectorQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Unit;

        void IVectorQuantityRecordBuilder.WithUnit(ITypeSymbol unit, ExpressionSyntax syntax)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Unit = unit;
            Target.Syntactic.Unit = syntax;
            Tracker = Tracker.WithUnit();
        }

        void IVectorQuantityRecordBuilder.WithDimension(int dimension, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Dimension = dimension;
            Target.Syntactic.Dimension = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool Unit { get; private init; }

            public BuildTracker WithUnit() => this with { Unit = true };
        }

        private sealed class VectorQuantityRecord : IVectorQuantityRecord
        {
            public SyntacticVectorQuantityRecord Syntactic { get; }

            public ITypeSymbol Unit { get; set; } = null!;
            public OneOf<None, int> Dimension { get; set; }

            public VectorQuantityRecord(SyntacticVectorQuantityRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticVectorQuantityRecord IVectorQuantityRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticVectorQuantityRecord : ASyntacticRecord, ISyntacticVectorQuantityRecord
        {
            public ExpressionSyntax Unit { get; set; } = null!;
            public OneOf<None, ExpressionSyntax> Dimension { get; set; }

            public SyntacticVectorQuantityRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
