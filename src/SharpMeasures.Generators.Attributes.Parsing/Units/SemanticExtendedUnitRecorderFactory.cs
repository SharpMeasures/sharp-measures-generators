namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Units;

using System;

/// <inheritdoc cref="ISemanticExtendedUnitRecorderFactory"/>
public sealed class SemanticExtendedUnitRecorderFactory : ISemanticExtendedUnitRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticExtendedUnitRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticExtendedUnitRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="ExtendedUnitAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="ExtendedUnitAttribute{TOriginal}"/> to recorders.</param>
    public SemanticExtendedUnitRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticExtendedUnitRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticExtendedUnitRecord> ISemanticRecorderFactory<ISemanticExtendedUnitRecord>.Create()
    {
        var recordBuilder = new ExtendedUnitRecordBuilder();

        return Factory.Create<ISemanticExtendedUnitRecord, ISemanticExtendedUnitRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class ExtendedUnitRecordBuilder : ARecordBuilder<ISemanticExtendedUnitRecord>, ISemanticExtendedUnitRecordBuilder
    {
        private ExtendedUnitRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public ExtendedUnitRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticExtendedUnitRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISemanticExtendedUnitRecordBuilder.WithOriginal(ITypeSymbol original)
        {
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            VerifyCanModify();

            Target.Original = original;
            Tracker = Tracker.WithOriginal();
        }

        private readonly struct BuildTracker
        {
            public bool Original { get; private init; }

            public BuildTracker WithOriginal() => this with { Original = true };
        }

        private sealed class ExtendedUnitRecord : ISemanticExtendedUnitRecord
        {
            public ITypeSymbol Original { get; set; } = null!;
        }
    }
}
