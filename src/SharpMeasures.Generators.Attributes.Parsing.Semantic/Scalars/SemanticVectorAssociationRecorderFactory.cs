namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="ISemanticVectorAssociationRecorderFactory"/>
public sealed class SemanticVectorAssociationRecorderFactory : ISemanticVectorAssociationRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticVectorAssociationRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticVectorAssociationRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorAssociationAttribute{TVector}"/> to recorders.</param>
    public SemanticVectorAssociationRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticVectorAssociationRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticVectorAssociationRecord> ISemanticRecorderFactory<ISemanticVectorAssociationRecord>.Create()
    {
        var recordBuilder = new VectorAssociationRecordBuilder();

        return Factory.Create<ISemanticVectorAssociationRecord, ISemanticVectorAssociationRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorAssociationRecordBuilder : ARecordBuilder<ISemanticVectorAssociationRecord>, ISemanticVectorAssociationRecordBuilder
    {
        private VectorAssociationRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public VectorAssociationRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticVectorAssociationRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.VectorQuantity;

        void ISemanticVectorAssociationRecordBuilder.WithVectorQuantity(ITypeSymbol vectorQuantity)
        {
            if (vectorQuantity is null)
            {
                throw new ArgumentNullException(nameof(vectorQuantity));
            }

            VerifyCanModify();

            Target.VectorQuantity = vectorQuantity;
            Tracker = Tracker.WithVectorQuantity();
        }

        private readonly struct BuildTracker
        {
            public bool VectorQuantity { get; private init; }

            public BuildTracker WithVectorQuantity() => this with { VectorQuantity = true };
        }

        private sealed class VectorAssociationRecord : ISemanticVectorAssociationRecord
        {
            public ITypeSymbol VectorQuantity { get; set; } = null!;
        }
    }
}
