namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="IVectorAssociationRecorderFactory"/>
public sealed class VectorAssociationRecorderFactory : IVectorAssociationRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IVectorAssociationRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="VectorAssociationRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorAssociationAttribute{TVector}"/> to recorders.</param>
    public VectorAssociationRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IVectorAssociationRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IVectorAssociationRecord> IVectorAssociationRecorderFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new VectorAssociationRecordBuilder(attributeSyntax);

        return Factory.Create<IVectorAssociationRecord, IVectorAssociationRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorAssociationRecordBuilder : ARecordBuilder<IVectorAssociationRecord>, IVectorAssociationRecordBuilder
    {
        private VectorAssociationRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public VectorAssociationRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticVectorAssociationRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IVectorAssociationRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.VectorQuantity;

        void IVectorAssociationRecordBuilder.WithVectorQuantity(ITypeSymbol vectorQuantity, ExpressionSyntax syntax)
        {
            if (vectorQuantity is null)
            {
                throw new ArgumentNullException(nameof(vectorQuantity));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.VectorQuantity = vectorQuantity;
            Target.Syntactic.VectorQuantity = syntax;
            Tracker = Tracker.WithVectorQuantity();
        }

        private readonly struct BuildTracker
        {
            public bool VectorQuantity { get; private init; }

            public BuildTracker WithVectorQuantity() => this with { VectorQuantity = true };
        }

        private sealed class VectorAssociationRecord : IVectorAssociationRecord
        {
            public SyntacticVectorAssociationRecord Syntactic { get; }

            public ITypeSymbol VectorQuantity { get; set; } = null!;

            public VectorAssociationRecord(SyntacticVectorAssociationRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticVectorAssociationRecord IVectorAssociationRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticVectorAssociationRecord : ASyntacticRecord, ISyntacticVectorAssociationRecord
        {
            public ExpressionSyntax VectorQuantity { get; set; } = null!;

            public SyntacticVectorAssociationRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
