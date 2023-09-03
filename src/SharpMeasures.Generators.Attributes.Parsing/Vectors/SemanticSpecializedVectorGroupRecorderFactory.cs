namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISemanticSpecializedVectorGroupRecorderFactory"/>
public sealed class SemanticSpecializedVectorGroupRecorderFactory : ISemanticSpecializedVectorGroupRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticSpecializedVectorGroupRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticSpecializedVectorGroupRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/> to recorders.</param>
    public SemanticSpecializedVectorGroupRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticSpecializedVectorGroupRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticSpecializedVectorGroupRecord> ISemanticSpecializedVectorGroupRecorderFactory.Create()
    {
        var recordBuilder = new SpecializedVectorGroupRecordBuilder();

        return Factory.Create<ISemanticSpecializedVectorGroupRecord, ISemanticSpecializedVectorGroupRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SpecializedVectorGroupRecordBuilder : ARecordBuilder<ISemanticSpecializedVectorGroupRecord>, ISemanticSpecializedVectorGroupRecordBuilder
    {
        private SpecializedVectorGroupRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public SpecializedVectorGroupRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticSpecializedVectorGroupRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISemanticSpecializedVectorGroupRecordBuilder.WithOriginal(ITypeSymbol original)
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

        private sealed class SpecializedVectorGroupRecord : ISemanticSpecializedVectorGroupRecord
        {
            public ITypeSymbol Original { get; set; } = null!;
        }
    }
}
