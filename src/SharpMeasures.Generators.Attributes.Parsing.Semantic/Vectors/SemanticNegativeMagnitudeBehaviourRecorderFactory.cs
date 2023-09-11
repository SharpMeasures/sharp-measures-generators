namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISemanticNegativeMagnitudeBehaviourRecorderFactory"/>
public sealed class SemanticNegativeMagnitudeBehaviourRecorderFactory : ISemanticNegativeMagnitudeBehaviourRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticNegativeMagnitudeBehaviourRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticNegativeMagnitudeBehaviourRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="NegativeMagnitudeBehaviourAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="NegativeMagnitudeBehaviourAttribute"/> to recorders.</param>
    public SemanticNegativeMagnitudeBehaviourRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticNegativeMagnitudeBehaviourRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticNegativeMagnitudeBehaviourRecord> ISemanticRecorderFactory<ISemanticNegativeMagnitudeBehaviourRecord>.Create()
    {
        var recordBuilder = new NegativeMagnitudeBehaviourRecordBuilder();

        return Factory.Create<ISemanticNegativeMagnitudeBehaviourRecord, ISemanticNegativeMagnitudeBehaviourRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class NegativeMagnitudeBehaviourRecordBuilder : ARecordBuilder<ISemanticNegativeMagnitudeBehaviourRecord>, ISemanticNegativeMagnitudeBehaviourRecordBuilder
    {
        private NegativeMagnitudeBehaviourRecord Target { get; } = new();

        public NegativeMagnitudeBehaviourRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticNegativeMagnitudeBehaviourRecord GetRecord() => Target;

        void ISemanticNegativeMagnitudeBehaviourRecordBuilder.WithBehaviour(DisallowNegativeBehaviour behaviour)
        {
            VerifyCanModify();

            Target.Behaviour = behaviour;
        }

        private sealed class NegativeMagnitudeBehaviourRecord : ISemanticNegativeMagnitudeBehaviourRecord
        {
            public OneOf<None, DisallowNegativeBehaviour> Behaviour { get; set; }
        }
    }
}
