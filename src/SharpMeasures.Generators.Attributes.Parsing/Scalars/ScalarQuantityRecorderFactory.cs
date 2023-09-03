namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="IScalarQuantityRecorderFactory"/>
public sealed class ScalarQuantityRecorderFactory : IScalarQuantityRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IScalarQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="ScalarQuantityRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="ScalarQuantityAttribute{TUnit}"/> to recorders.</param>
    public ScalarQuantityRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IScalarQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IScalarQuantityRecord> IScalarQuantityRecorderFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new ScalarQuantityRecordBuilder(attributeSyntax);

        return Factory.Create<IScalarQuantityRecord, IScalarQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class ScalarQuantityRecordBuilder : ARecordBuilder<IScalarQuantityRecord>, IScalarQuantityRecordBuilder
    {
        private ScalarQuantityRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public ScalarQuantityRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticScalarQuantityRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IScalarQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Unit;

        void IScalarQuantityRecordBuilder.WithUnit(ITypeSymbol unit, ExpressionSyntax syntax)
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

        void IScalarQuantityRecordBuilder.WithBiased(bool biased, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Biased = biased;
            Target.Syntactic.Biased = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool Unit { get; private init; }

            public BuildTracker WithUnit() => this with { Unit = true };
        }

        private sealed class ScalarQuantityRecord : IScalarQuantityRecord
        {
            public SyntacticScalarQuantityRecord Syntactic { get; }

            public ITypeSymbol Unit { get; set; } = null!;
            public OneOf<None, bool> Biased { get; set; }

            public ScalarQuantityRecord(SyntacticScalarQuantityRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticScalarQuantityRecord IScalarQuantityRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticScalarQuantityRecord : ASyntacticRecord, ISyntacticScalarQuantityRecord
        {
            public ExpressionSyntax Unit { get; set; } = null!;
            public OneOf<None, ExpressionSyntax> Biased { get; set; }

            public SyntacticScalarQuantityRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
