namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="ISpecializedScalarQuantityRecorderFactory"/>
public sealed class SpecializedScalarQuantityRecorderFactory : ISpecializedScalarQuantityRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<ISpecializedScalarQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SpecializedScalarQuantityRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/> to recorders.</param>
    public SpecializedScalarQuantityRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<ISpecializedScalarQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<ISpecializedScalarQuantityRecord> IRecorderFactory<ISpecializedScalarQuantityRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new SpecializedScalarQuantityRecordBuilder(attributeSyntax);

        return Factory.Create<ISpecializedScalarQuantityRecord, ISpecializedScalarQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SpecializedScalarQuantityRecordBuilder : ARecordBuilder<ISpecializedScalarQuantityRecord>, ISpecializedScalarQuantityRecordBuilder
    {
        private SpecializedScalarQuantityRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public SpecializedScalarQuantityRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticSpecializedScalarQuantityRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override ISpecializedScalarQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISpecializedScalarQuantityRecordBuilder.WithOriginal(ITypeSymbol original, ExpressionSyntax syntax)
        {
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Original = original;
            Target.Syntactic.Original = syntax;
            Tracker = Tracker.WithOriginal();
        }

        private readonly struct BuildTracker
        {
            public bool Original { get; private init; }

            public BuildTracker WithOriginal() => this with { Original = true };
        }

        private sealed class SpecializedScalarQuantityRecord : ISpecializedScalarQuantityRecord
        {
            public SyntacticSpecializedScalarQuantityRecord Syntactic { get; }

            public ITypeSymbol Original { get; set; } = null!;

            public SpecializedScalarQuantityRecord(SyntacticSpecializedScalarQuantityRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticSpecializedScalarQuantityRecord ISpecializedScalarQuantityRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticSpecializedScalarQuantityRecord : ASyntacticRecord, ISyntacticSpecializedScalarQuantityRecord
        {
            public ExpressionSyntax Original { get; set; } = null!;

            public SyntacticSpecializedScalarQuantityRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
