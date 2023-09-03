namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="ISemanticQuantitySumRecorderFactory"/>
public sealed class SemanticQuantitySumRecorderFactory : ISemanticQuantitySumRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticQuantitySumRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticQuantitySumRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="QuantitySumAttribute{TSum}"/> to recorders.</param>
    public SemanticQuantitySumRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticQuantitySumRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticQuantitySumRecord> ISemanticQuantitySumRecorderFactory.Create()
    {
        var recordBuilder = new QuantitySumRecordBuilder();

        return Factory.Create<ISemanticQuantitySumRecord, ISemanticQuantitySumRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class QuantitySumRecordBuilder : ARecordBuilder<ISemanticQuantitySumRecord>, ISemanticQuantitySumRecordBuilder
    {
        private QuantitySumRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public QuantitySumRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticQuantitySumRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Sum;

        void ISemanticQuantitySumRecordBuilder.WithSum(ITypeSymbol sum)
        {
            if (sum is null)
            {
                throw new ArgumentNullException(nameof(sum));
            }

            VerifyCanModify();

            Target.Sum = sum;
            Tracker = Tracker.WithSum();
        }

        private readonly struct BuildTracker
        {
            public bool Sum { get; private init; }

            public BuildTracker WithSum() => this with { Sum = true };
        }

        private sealed class QuantitySumRecord : ISemanticQuantitySumRecord
        {
            public ITypeSymbol Sum { get; set; } = null!;
        }
    }
}
