namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Units;

using System;

/// <inheritdoc cref="ISemanticUnitInstanceRecorderFactory"/>
public sealed class SemanticUnitInstanceRecorderFactory : ISemanticUnitInstanceRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticUnitInstanceRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticUnitInstanceRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="UnitInstanceAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="UnitInstanceAttribute"/> to recorders.</param>
    public SemanticUnitInstanceRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticUnitInstanceRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticUnitInstanceRecord> ISemanticRecorderFactory<ISemanticUnitInstanceRecord>.Create()
    {
        var recordBuilder = new SemanticUnitInstanceRecordBuilder();

        return Factory.Create<ISemanticUnitInstanceRecord, ISemanticUnitInstanceRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SemanticUnitInstanceRecordBuilder : ARecordBuilder<ISemanticUnitInstanceRecord>, ISemanticUnitInstanceRecordBuilder
    {
        private SemanticUnitInstanceRecord Target { get; } = new();

        public SemanticUnitInstanceRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticUnitInstanceRecord GetRecord() => Target;

        void ISemanticUnitInstanceRecordBuilder.WithName(string? name)
        {
            VerifyCanModify();

            Target.Name = name;
        }

        void ISemanticUnitInstanceRecordBuilder.WithPluralForm(string? pluralForm)
        {
            VerifyCanModify();

            Target.PluralForm = pluralForm;
        }

        private sealed class SemanticUnitInstanceRecord : ISemanticUnitInstanceRecord
        {
            public OneOf<None, string?> Name { get; set; }
            public OneOf<None, string?> PluralForm { get; set; }
        }
    }
}
