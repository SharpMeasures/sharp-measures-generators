namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="ISpecializedUnitlessQuantityRecorderFactory"/>
public sealed class SpecializedUnitlessQuantityRecorderFactory : ISpecializedUnitlessQuantityRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<ISpecializedUnitlessQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SpecializedUnitlessQuantityRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/> to recorders.</param>
    public SpecializedUnitlessQuantityRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<ISpecializedUnitlessQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<ISpecializedUnitlessQuantityRecord> IRecorderFactory<ISpecializedUnitlessQuantityRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new SpecializedUnitlessQuantityRecordBuilder(attributeSyntax);

        return Factory.Create<ISpecializedUnitlessQuantityRecord, ISpecializedUnitlessQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SpecializedUnitlessQuantityRecordBuilder : ARecordBuilder<ISpecializedUnitlessQuantityRecord>, ISpecializedUnitlessQuantityRecordBuilder
    {
        private SpecializedUnitlessQuantityRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public SpecializedUnitlessQuantityRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticSpecializedUnitlessQuantityRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override ISpecializedUnitlessQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISpecializedUnitlessQuantityRecordBuilder.WithOriginal(ITypeSymbol original, ExpressionSyntax syntax)
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

        private sealed class SpecializedUnitlessQuantityRecord : ISpecializedUnitlessQuantityRecord
        {
            public SyntacticSpecializedUnitlessQuantityRecord Syntactic { get; }

            public ITypeSymbol Original { get; set; } = null!;

            public SpecializedUnitlessQuantityRecord(SyntacticSpecializedUnitlessQuantityRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticSpecializedUnitlessQuantityRecord ISpecializedUnitlessQuantityRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticSpecializedUnitlessQuantityRecord : ASyntacticRecord, ISyntacticSpecializedUnitlessQuantityRecord
        {
            public ExpressionSyntax Original { get; set; } = null!;

            public SyntacticSpecializedUnitlessQuantityRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
