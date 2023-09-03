namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISemanticSpecializedVectorQuantityRecorderFactory"/>
public sealed class SemanticSpecializedVectorQuantityRecorderFactory : ISemanticSpecializedVectorQuantityRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticSpecializedVectorQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticSpecializedVectorQuantityRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/> to recorders.</param>
    public SemanticSpecializedVectorQuantityRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticSpecializedVectorQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticSpecializedVectorQuantityRecord> ISemanticRecorderFactory<ISemanticSpecializedVectorQuantityRecord>.Create()
    {
        var recordBuilder = new SpecializedVectorQuantityRecordBuilder();

        return Factory.Create<ISemanticSpecializedVectorQuantityRecord, ISemanticSpecializedVectorQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SpecializedVectorQuantityRecordBuilder : ARecordBuilder<ISemanticSpecializedVectorQuantityRecord>, ISemanticSpecializedVectorQuantityRecordBuilder
    {
        private SpecializedVectorQuantityRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public SpecializedVectorQuantityRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticSpecializedVectorQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISemanticSpecializedVectorQuantityRecordBuilder.WithOriginal(ITypeSymbol original)
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

        private sealed class SpecializedVectorQuantityRecord : ISemanticSpecializedVectorQuantityRecord
        {
            public ITypeSymbol Original { get; set; } = null!;
        }
    }
}
