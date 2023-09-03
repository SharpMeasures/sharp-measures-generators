namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="IQuantitySumRecorderFactory"/>
public sealed class QuantitySumRecorderFactory : IQuantitySumRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IQuantitySumRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="QuantitySumRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="QuantitySumAttribute{TSum}"/> to recorders.</param>
    public QuantitySumRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IQuantitySumRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IQuantitySumRecord> IRecorderFactory<IQuantitySumRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new QuantitySumRecordBuilder(attributeSyntax);

        return Factory.Create<IQuantitySumRecord, IQuantitySumRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class QuantitySumRecordBuilder : ARecordBuilder<IQuantitySumRecord>, IQuantitySumRecordBuilder
    {
        private QuantitySumRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public QuantitySumRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticQuantitySumRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IQuantitySumRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Sum;

        void IQuantitySumRecordBuilder.WithSum(ITypeSymbol sum, ExpressionSyntax syntax)
        {
            if (sum is null)
            {
                throw new ArgumentNullException(nameof(sum));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Sum = sum;
            Target.Syntactic.Sum = syntax;
            Tracker = Tracker.WithSum();
        }

        private readonly struct BuildTracker
        {
            public bool Sum { get; private init; }

            public BuildTracker WithSum() => this with { Sum = true };
        }

        private sealed class QuantitySumRecord : IQuantitySumRecord
        {
            public SyntacticQuantitySumRecord Syntactic { get; }

            public ITypeSymbol Sum { get; set; } = null!;

            public QuantitySumRecord(SyntacticQuantitySumRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticQuantitySumRecord IQuantitySumRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticQuantitySumRecord : ASyntacticRecord, ISyntacticQuantitySumRecord
        {
            public ExpressionSyntax Sum { get; set; } = null!;

            public SyntacticQuantitySumRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
