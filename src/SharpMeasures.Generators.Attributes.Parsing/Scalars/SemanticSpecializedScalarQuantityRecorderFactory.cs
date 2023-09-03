namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="ISemanticSpecializedScalarQuantityRecorderFactory"/>
public sealed class SemanticSpecializedScalarQuantityRecorderFactory : ISemanticSpecializedScalarQuantityRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticSpecializedScalarQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticSpecializedScalarQuantityRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/> to recorders.</param>
    public SemanticSpecializedScalarQuantityRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticSpecializedScalarQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticSpecializedScalarQuantityRecord> ISemanticSpecializedScalarQuantityRecorderFactory.Create()
    {
        var recordBuilder = new SpecializedScalarQuantityRecordBuilder();

        return Factory.Create<ISemanticSpecializedScalarQuantityRecord, ISemanticSpecializedScalarQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SpecializedScalarQuantityRecordBuilder : ARecordBuilder<ISemanticSpecializedScalarQuantityRecord>, ISemanticSpecializedScalarQuantityRecordBuilder
    {
        private SpecializedScalarQuantityRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public SpecializedScalarQuantityRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticSpecializedScalarQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISemanticSpecializedScalarQuantityRecordBuilder.WithOriginal(ITypeSymbol original)
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

        private sealed class SpecializedScalarQuantityRecord : ISemanticSpecializedScalarQuantityRecord
        {
            public ITypeSymbol Original { get; set; } = null!;
        }
    }
}
