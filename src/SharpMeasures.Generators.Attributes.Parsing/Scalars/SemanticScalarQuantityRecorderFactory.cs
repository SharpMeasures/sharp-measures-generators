namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="ISemanticScalarQuantityRecorderFactory"/>
public sealed class SemanticScalarQuantityRecorderFactory : ISemanticScalarQuantityRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticScalarQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticScalarQuantityRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="ScalarQuantityAttribute{TUnit}"/> to recorders.</param>
    public SemanticScalarQuantityRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticScalarQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticScalarQuantityRecord> ISemanticScalarQuantityRecorderFactory.Create()
    {
        var recordBuilder = new ScalarQuantityRecordBuilder();

        return Factory.Create<ISemanticScalarQuantityRecord, ISemanticScalarQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class ScalarQuantityRecordBuilder : ARecordBuilder<ISemanticScalarQuantityRecord>, ISemanticScalarQuantityRecordBuilder
    {
        private ScalarQuantityRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public ScalarQuantityRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticScalarQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Unit;

        void ISemanticScalarQuantityRecordBuilder.WithUnit(ITypeSymbol unit)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            VerifyCanModify();

            Target.Unit = unit;
            Tracker = Tracker.WithUnit();
        }

        void ISemanticScalarQuantityRecordBuilder.WithBiased(bool biased)
        {
            VerifyCanModify();

            Target.Biased = biased;
        }

        private readonly struct BuildTracker
        {
            public bool Unit { get; private init; }

            public BuildTracker WithUnit() => this with { Unit = true };
        }

        private sealed class ScalarQuantityRecord : ISemanticScalarQuantityRecord
        {
            public ITypeSymbol Unit { get; set; } = null!;
            public OneOf<None, bool> Biased { get; set; }
        }
    }
}
