namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISemanticScalarAssociationRecorderFactory"/>
public sealed class SemanticScalarAssociationRecorderFactory : ISemanticScalarAssociationRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticScalarAssociationRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticScalarAssociationRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="ScalarAssociationAttribute{TScalar}"/> to recorders.</param>
    public SemanticScalarAssociationRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticScalarAssociationRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticScalarAssociationRecord> ISemanticRecorderFactory<ISemanticScalarAssociationRecord>.Create()
    {
        var recordBuilder = new ScalarAssociationRecordBuilder();

        return Factory.Create<ISemanticScalarAssociationRecord, ISemanticScalarAssociationRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class ScalarAssociationRecordBuilder : ARecordBuilder<ISemanticScalarAssociationRecord>, ISemanticScalarAssociationRecordBuilder
    {
        private ScalarAssociationRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public ScalarAssociationRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticScalarAssociationRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.ScalarQuantity;

        void ISemanticScalarAssociationRecordBuilder.WithScalarQuantity(ITypeSymbol scalarQuantity)
        {
            if (scalarQuantity is null)
            {
                throw new ArgumentNullException(nameof(scalarQuantity));
            }

            VerifyCanModify();

            Target.ScalarQuantity = scalarQuantity;
            Tracker = Tracker.WithScalarQuantity();
        }

        void ISemanticScalarAssociationRecordBuilder.WithAsComponents(bool asComponents)
        {
            VerifyCanModify();

            Target.AsComponents = asComponents;
        }

        void ISemanticScalarAssociationRecordBuilder.WithAsMagnitude(bool asMagnitude)
        {
            VerifyCanModify();

            Target.AsMagnitude = asMagnitude;
        }

        private readonly struct BuildTracker
        {
            public bool ScalarQuantity { get; private init; }

            public BuildTracker WithScalarQuantity() => this with { ScalarQuantity = true };
        }

        private sealed class ScalarAssociationRecord : ISemanticScalarAssociationRecord
        {
            public ITypeSymbol ScalarQuantity { get; set; } = null!;
            public OneOf<None, bool> AsComponents { get; set; }
            public OneOf<None, bool> AsMagnitude { get; set; }
        }
    }
}
