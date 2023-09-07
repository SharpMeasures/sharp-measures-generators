namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISpecializedVectorGroupRecorderFactory"/>
public sealed class SpecializedVectorGroupRecorderFactory : ISpecializedVectorGroupRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<ISpecializedVectorGroupRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SpecializedVectorGroupRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="SpecializedVectorGroupAttribute{TOriginal}"/> to recorders.</param>
    public SpecializedVectorGroupRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<ISpecializedVectorGroupRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<ISpecializedVectorGroupRecord> IRecorderFactory<ISpecializedVectorGroupRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new SpecializedVectorGroupRecordBuilder(attributeSyntax);

        return Factory.Create<ISpecializedVectorGroupRecord, ISpecializedVectorGroupRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class SpecializedVectorGroupRecordBuilder : ARecordBuilder<ISpecializedVectorGroupRecord>, ISpecializedVectorGroupRecordBuilder
    {
        private SpecializedVectorGroupRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public SpecializedVectorGroupRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticSpecializedVectorGroupRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override ISpecializedVectorGroupRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void ISpecializedVectorGroupRecordBuilder.WithOriginal(ITypeSymbol original, ExpressionSyntax syntax)
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

        private sealed class SpecializedVectorGroupRecord : ISpecializedVectorGroupRecord
        {
            public SyntacticSpecializedVectorGroupRecord Syntactic { get; }

            public ITypeSymbol Original { get; set; } = null!;

            public SpecializedVectorGroupRecord(SyntacticSpecializedVectorGroupRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticSpecializedVectorGroupRecord ISpecializedVectorGroupRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticSpecializedVectorGroupRecord : ASyntacticRecord, ISyntacticSpecializedVectorGroupRecord
        {
            public ExpressionSyntax Original { get; set; } = null!;

            public SyntacticSpecializedVectorGroupRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
