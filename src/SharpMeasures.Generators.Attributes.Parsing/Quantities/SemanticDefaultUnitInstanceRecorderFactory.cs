namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="ISemanticDefaultUnitInstanceRecorderFactory"/>
public sealed class SemanticDefaultUnitInstanceRecorderFactory : ISemanticDefaultUnitInstanceRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticDefaultUnitInstanceRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="DefaultUnitInstanceAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="DefaultUnitInstanceAttribute"/> to recorders.</param>
    public SemanticDefaultUnitInstanceRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticDefaultUnitInstanceRecord> ISemanticRecorderFactory<ISemanticDefaultUnitInstanceRecord>.Create()
    {
        return Factory.Create<ISemanticDefaultUnitInstanceRecord, ISemanticDefaultUnitInstanceRecordBuilder>(Mapper, new DefaultUnitInstanceRecordBuilder());
    }

    private sealed class DefaultUnitInstanceRecordBuilder : ARecordBuilder<ISemanticDefaultUnitInstanceRecord>, ISemanticDefaultUnitInstanceRecordBuilder
    {
        private DefaultUnitInstanceRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public DefaultUnitInstanceRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticDefaultUnitInstanceRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.UnitInstance;

        void ISemanticDefaultUnitInstanceRecordBuilder.WithUnitInstance(string? unitInstance)
        {
            VerifyCanModify();

            Target.UnitInstance = unitInstance;
            Tracker = Tracker = Tracker.WithUnitInstance();
        }

        void ISemanticDefaultUnitInstanceRecordBuilder.WithSymbol(string? symbol)
        {
            VerifyCanModify();

            Target.Symbol = symbol;
        }

        private readonly struct BuildTracker
        {
            public bool UnitInstance { get; private init; }

            public BuildTracker WithUnitInstance() => this with { UnitInstance = true };
        }

        private sealed class DefaultUnitInstanceRecord : ISemanticDefaultUnitInstanceRecord
        {
            public string? UnitInstance { get; set; }
            public OneOf<None, string?> Symbol { get; set; }
        }
    }
}
