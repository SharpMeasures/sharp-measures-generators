namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="ISemanticDisallowNegativeRecorderFactory"/>
public sealed class SemanticDisallowNegativeRecorderFactory : ISemanticDisallowNegativeRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticDisallowNegativeRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticDisallowNegativeRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="DisallowNegativeAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="DisallowNegativeAttribute"/> to recorders.</param>
    public SemanticDisallowNegativeRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticDisallowNegativeRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticDisallowNegativeRecord> ISemanticRecorderFactory<ISemanticDisallowNegativeRecord>.Create()
    {
        var recordBuilder = new DisallowNegativeRecordBuilder();

        return Factory.Create<ISemanticDisallowNegativeRecord, ISemanticDisallowNegativeRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class DisallowNegativeRecordBuilder : ARecordBuilder<ISemanticDisallowNegativeRecord>, ISemanticDisallowNegativeRecordBuilder
    {
        private DisallowNegativeRecord Target { get; } = new();

        public DisallowNegativeRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticDisallowNegativeRecord GetRecord() => Target;

        void ISemanticDisallowNegativeRecordBuilder.WithBehaviour(DisallowNegativeBehaviour behaviour)
        {
            VerifyCanModify();

            Target.Behaviour = behaviour;
        }

        private sealed class DisallowNegativeRecord : ISemanticDisallowNegativeRecord
        {
            public OneOf<None, DisallowNegativeBehaviour> Behaviour { get; set; }
        }
    }
}
