namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Units;

using System;

/// <inheritdoc cref="ISemanticUnitRecorderFactory"/>
public sealed class SemanticUnitRecorderFactory : ISemanticUnitRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticUnitRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticUnitRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="UnitAttribute{TScalar}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="UnitAttribute{TScalar}"/> to recorders.</param>
    public SemanticUnitRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticUnitRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticUnitRecord> ISemanticRecorderFactory<ISemanticUnitRecord>.Create()
    {
        var recordBuilder = new UnitRecordBuilder();

        return Factory.Create<ISemanticUnitRecord, ISemanticUnitRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class UnitRecordBuilder : ARecordBuilder<ISemanticUnitRecord>, ISemanticUnitRecordBuilder
    {
        private UnitRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public UnitRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticUnitRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.ScalarQuantity;

        void ISemanticUnitRecordBuilder.WithScalarQuantity(ITypeSymbol scalarQuantity)
        {
            if (scalarQuantity is null)
            {
                throw new ArgumentNullException(nameof(scalarQuantity));
            }

            VerifyCanModify();

            Target.ScalarQuantity = scalarQuantity;
            Tracker = Tracker.WithScalarQuantity();
        }

        void ISemanticUnitRecordBuilder.WithBiasTerm(bool biasTerm)
        {
            VerifyCanModify();

            Target.BiasTerm = biasTerm;
        }

        private readonly struct BuildTracker
        {
            public bool ScalarQuantity { get; private init; }

            public BuildTracker WithScalarQuantity() => this with { ScalarQuantity = true };
        }

        private sealed class UnitRecord : ISemanticUnitRecord
        {
            public ITypeSymbol ScalarQuantity { get; set; } = null!;
            public OneOf<None, bool> BiasTerm { get; set; }
        }
    }
}
