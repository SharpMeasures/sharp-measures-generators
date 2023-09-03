namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="ISemanticSpecializedUnitlessQuantityRecorderFactory"/>
public sealed class SemanticSpecializedUnitlessQuantityRecorderFactory : ISemanticSpecializedUnitlessQuantityRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticSpecializedUnitlessQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticSpecializedUnitlessQuantityRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="SpecializedUnitlessQuantityAttribute{TOriginal}"/> to recorders.</param>
    public SemanticSpecializedUnitlessQuantityRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticSpecializedUnitlessQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticSpecializedUnitlessQuantityRecord> ISemanticRecorderFactory<ISemanticSpecializedUnitlessQuantityRecord>.Create()
    {
        var recordBuilder = new SpecializedUnitlessQuantityRecordBuilder();

        return Factory.Create<ISemanticSpecializedUnitlessQuantityRecord, ISemanticSpecializedUnitlessQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SpecializedUnitlessQuantityRecordBuilder : ARecordBuilder<ISemanticSpecializedUnitlessQuantityRecord>, ISemanticSpecializedUnitlessQuantityRecordBuilder
    {
        private SpecializedUnitlessQuantityRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public SpecializedUnitlessQuantityRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticSpecializedUnitlessQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISemanticSpecializedUnitlessQuantityRecordBuilder.WithOriginal(ITypeSymbol original)
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

        private sealed class SpecializedUnitlessQuantityRecord : ISemanticSpecializedUnitlessQuantityRecord
        {
            public ITypeSymbol Original { get; set; } = null!;
        }
    }
}
