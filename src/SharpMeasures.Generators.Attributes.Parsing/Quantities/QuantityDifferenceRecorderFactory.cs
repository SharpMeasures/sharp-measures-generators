namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="IQuantityDifferenceRecorderFactory"/>
public sealed class QuantityDifferenceRecorderFactory : IQuantityDifferenceRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IQuantityDifferenceRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="QuantityDifferenceRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="QuantityDifferenceAttribute{TDifference}"/> to recorders.</param>
    public QuantityDifferenceRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IQuantityDifferenceRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IQuantityDifferenceRecord> IQuantityDifferenceRecorderFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new QuantityDifferenceRecordBuilder(attributeSyntax);

        return Factory.Create<IQuantityDifferenceRecord, IQuantityDifferenceRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class QuantityDifferenceRecordBuilder : ARecordBuilder<IQuantityDifferenceRecord>, IQuantityDifferenceRecordBuilder
    {
        private QuantityDifferenceRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public QuantityDifferenceRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticQuantityDifferenceRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IQuantityDifferenceRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Difference;

        void IQuantityDifferenceRecordBuilder.WithDifference(ITypeSymbol difference, ExpressionSyntax syntax)
        {
            if (difference is null)
            {
                throw new ArgumentNullException(nameof(difference));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Difference = difference;
            Target.Syntactic.Difference = syntax;
            Tracker = Tracker.WithDifference();
        }

        private readonly struct BuildTracker
        {
            public bool Difference { get; private init; }

            public BuildTracker WithDifference() => this with { Difference = true };
        }

        private sealed class QuantityDifferenceRecord : IQuantityDifferenceRecord
        {
            public SyntacticQuantityDifferenceRecord Syntactic { get; }

            public ITypeSymbol Difference { get; set; } = null!;

            public QuantityDifferenceRecord(SyntacticQuantityDifferenceRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticQuantityDifferenceRecord IQuantityDifferenceRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticQuantityDifferenceRecord : ASyntacticRecord, ISyntacticQuantityDifferenceRecord
        {
            public ExpressionSyntax Difference { get; set; } = null!;

            public SyntacticQuantityDifferenceRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
