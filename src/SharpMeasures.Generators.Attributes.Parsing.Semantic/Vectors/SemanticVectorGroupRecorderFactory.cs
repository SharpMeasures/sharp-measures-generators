namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISemanticVectorGroupRecorderFactory"/>
public sealed class SemanticVectorGroupRecorderFactory : ISemanticVectorGroupRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticVectorGroupRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticVectorGroupRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorGroupAttribute{TUnit}"/> to recorders.</param>
    public SemanticVectorGroupRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticVectorGroupRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticVectorGroupRecord> ISemanticRecorderFactory<ISemanticVectorGroupRecord>.Create()
    {
        var recordBuilder = new VectorGroupRecordBuilder();

        return Factory.Create<ISemanticVectorGroupRecord, ISemanticVectorGroupRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorGroupRecordBuilder : ARecordBuilder<ISemanticVectorGroupRecord>, ISemanticVectorGroupRecordBuilder
    {
        private VectorGroupRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public VectorGroupRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticVectorGroupRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Unit;

        void ISemanticVectorGroupRecordBuilder.WithUnit(ITypeSymbol unit)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            VerifyCanModify();

            Target.Unit = unit;
            Tracker = Tracker.WithUnit();
        }

        private readonly struct BuildTracker
        {
            public bool Unit { get; private init; }

            public BuildTracker WithUnit() => this with { Unit = true };
        }

        private sealed class VectorGroupRecord : ISemanticVectorGroupRecord
        {
            public ITypeSymbol Unit { get; set; } = null!;
        }
    }
}
