namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="IVectorGroupRecorderFactory"/>
public sealed class VectorGroupRecorderFactory : IVectorGroupRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IVectorGroupRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="VectorGroupRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="VectorGroupAttribute{TUnit}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorGroupAttribute{TUnit}"/> to recorders.</param>
    public VectorGroupRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IVectorGroupRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IVectorGroupRecord> IRecorderFactory<IVectorGroupRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new VectorGroupRecordBuilder(attributeSyntax);

        return Factory.Create<IVectorGroupRecord, IVectorGroupRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorGroupRecordBuilder : ARecordBuilder<IVectorGroupRecord>, IVectorGroupRecordBuilder
    {
        private VectorGroupRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public VectorGroupRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticVectorGroupRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IVectorGroupRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Unit;

        void IVectorGroupRecordBuilder.WithUnit(ITypeSymbol unit, ExpressionSyntax syntax)
        {
            if (unit is null)
            {
                throw new ArgumentNullException(nameof(unit));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Unit = unit;
            Target.Syntactic.Unit = syntax;
            Tracker = Tracker.WithUnit();
        }

        private readonly struct BuildTracker
        {
            public bool Unit { get; private init; }

            public BuildTracker WithUnit() => this with { Unit = true };
        }

        private sealed class VectorGroupRecord : IVectorGroupRecord
        {
            public SyntacticVectorGroupRecord Syntactic { get; }

            public ITypeSymbol Unit { get; set; } = null!;

            public VectorGroupRecord(SyntacticVectorGroupRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticVectorGroupRecord IVectorGroupRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticVectorGroupRecord : ASyntacticRecord, ISyntacticVectorGroupRecord
        {
            public ExpressionSyntax Unit { get; set; } = null!;

            public SyntacticVectorGroupRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
