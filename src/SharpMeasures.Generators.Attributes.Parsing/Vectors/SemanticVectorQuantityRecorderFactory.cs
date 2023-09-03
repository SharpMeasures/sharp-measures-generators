namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISemanticVectorQuantityRecorderFactory"/>
public sealed class SemanticVectorQuantityRecorderFactory : ISemanticVectorQuantityRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticVectorQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticVectorQuantityRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorQuantityAttribute{TUnit}"/> to recorders.</param>
    public SemanticVectorQuantityRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticVectorQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticVectorQuantityRecord> ISemanticVectorQuantityRecorderFactory.Create()
    {
        var recordBuilder = new VectorQuantityRecordBuilder();

        return Factory.Create<ISemanticVectorQuantityRecord, ISemanticVectorQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorQuantityRecordBuilder : ARecordBuilder<ISemanticVectorQuantityRecord>, ISemanticVectorQuantityRecordBuilder
    {
        private VectorQuantityRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public VectorQuantityRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticVectorQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Unit;

        void ISemanticVectorQuantityRecordBuilder.WithUnit(ITypeSymbol unit)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            VerifyCanModify();

            Target.Unit = unit;
            Tracker = Tracker.WithUnit();
        }

        void ISemanticVectorQuantityRecordBuilder.WithDimension(int dimension)
        {
            VerifyCanModify();

            Target.Dimension = dimension;
        }

        private readonly struct BuildTracker
        {
            public bool Unit { get; private init; }

            public BuildTracker WithUnit() => this with { Unit = true };
        }

        private sealed class VectorQuantityRecord : ISemanticVectorQuantityRecord
        {
            public ITypeSymbol Unit { get; set; } = null!;
            public OneOf<None, int> Dimension { get; set; }
        }
    }
}
