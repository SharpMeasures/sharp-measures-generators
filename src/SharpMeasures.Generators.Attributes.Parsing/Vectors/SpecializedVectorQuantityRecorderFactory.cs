namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISpecializedVectorQuantityRecorderFactory"/>
public sealed class SpecializedVectorQuantityRecorderFactory : ISpecializedVectorQuantityRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<ISpecializedVectorQuantityRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SpecializedVectorQuantityRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/> to recorders.</param>
    public SpecializedVectorQuantityRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<ISpecializedVectorQuantityRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<ISpecializedVectorQuantityRecord> IRecorderFactory<ISpecializedVectorQuantityRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new SpecializedVectorQuantityRecordBuilder(attributeSyntax);

        return Factory.Create<ISpecializedVectorQuantityRecord, ISpecializedVectorQuantityRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SpecializedVectorQuantityRecordBuilder : ARecordBuilder<ISpecializedVectorQuantityRecord>, ISpecializedVectorQuantityRecordBuilder
    {
        private SpecializedVectorQuantityRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public SpecializedVectorQuantityRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticSpecializedVectorQuantityRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override ISpecializedVectorQuantityRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISpecializedVectorQuantityRecordBuilder.WithOriginal(ITypeSymbol original, ExpressionSyntax syntax)
        {
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Original = original;
            Target.Syntactic.Original = syntax;
            Tracker = Tracker.WithOriginal();
        }

        private readonly struct BuildTracker
        {
            public bool Original { get; private init; }

            public BuildTracker WithOriginal() => this with { Original = true };
        }

        private sealed class SpecializedVectorQuantityRecord : ISpecializedVectorQuantityRecord
        {
            public SyntacticSpecializedVectorQuantityRecord Syntactic { get; }

            public ITypeSymbol Original { get; set; } = null!;

            public SpecializedVectorQuantityRecord(SyntacticSpecializedVectorQuantityRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticSpecializedVectorQuantityRecord ISpecializedVectorQuantityRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticSpecializedVectorQuantityRecord : ASyntacticRecord, ISyntacticSpecializedVectorQuantityRecord
        {
            public ExpressionSyntax Original { get; set; } = null!;

            public SyntacticSpecializedVectorQuantityRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
