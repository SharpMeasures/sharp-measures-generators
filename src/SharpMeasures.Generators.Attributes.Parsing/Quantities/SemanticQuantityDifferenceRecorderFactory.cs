namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="ISemanticQuantityDifferenceRecorderFactory"/>
public sealed class SemanticQuantityDifferenceRecorderFactory : ISemanticQuantityDifferenceRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticQuantityDifferenceRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticQuantityDifferenceRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="QuantityDifferenceAttribute{TDifference}"/> to recorders.</param>
    public SemanticQuantityDifferenceRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticQuantityDifferenceRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticQuantityDifferenceRecord> ISemanticQuantityDifferenceRecorderFactory.Create()
    {
        var recordBuilder = new QuantityDifferenceRecordBuilder();

        return Factory.Create<ISemanticQuantityDifferenceRecord, ISemanticQuantityDifferenceRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class QuantityDifferenceRecordBuilder : ARecordBuilder<ISemanticQuantityDifferenceRecord>, ISemanticQuantityDifferenceRecordBuilder
    {
        private QuantityDifferenceRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public QuantityDifferenceRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticQuantityDifferenceRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Difference;

        void ISemanticQuantityDifferenceRecordBuilder.WithDifference(ITypeSymbol difference)
        {
            if (difference is null)
            {
                throw new ArgumentNullException(nameof(difference));
            }

            VerifyCanModify();

            Target.Difference = difference;
            Tracker = Tracker.WithDifference();
        }

        private readonly struct BuildTracker
        {
            public bool Difference { get; private init; }

            public BuildTracker WithDifference() => this with { Difference = true };
        }

        private sealed class QuantityDifferenceRecord : ISemanticQuantityDifferenceRecord
        {
            public ITypeSymbol Difference { get; set; } = null!;
        }
    }
}
