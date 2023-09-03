namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Units;

using System;

/// <inheritdoc cref="IUnitRecorderFactory"/>
public sealed class UnitRecorderFactory : IUnitRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IUnitRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="UnitRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="UnitAttribute{TScalar}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="UnitAttribute{TScalar}"/> to recorders.</param>
    public UnitRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IUnitRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IUnitRecord> IRecorderFactory<IUnitRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new UnitRecordBuilder(attributeSyntax);

        return Factory.Create<IUnitRecord, IUnitRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class UnitRecordBuilder : ARecordBuilder<IUnitRecord>, IUnitRecordBuilder
    {
        private UnitRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public UnitRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticUnitRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IUnitRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.ScalarQuantity;

        void IUnitRecordBuilder.WithScalarQuantity(ITypeSymbol scalarQuantity, ExpressionSyntax syntax)
        {
            if (scalarQuantity is null)
            {
                throw new ArgumentNullException(nameof(scalarQuantity));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.ScalarQuantity = scalarQuantity;
            Target.Syntactic.ScalarQuantity = syntax;
            Tracker = Tracker.WithScalarQuantity();
        }

        void IUnitRecordBuilder.WithBiasTerm(bool biasTerm, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.BiasTerm = biasTerm;
            Target.Syntactic.BiasTerm = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool ScalarQuantity { get; private init; }

            public BuildTracker WithScalarQuantity() => this with { ScalarQuantity = true };
        }

        private sealed class UnitRecord : IUnitRecord
        {
            public SyntacticUnitRecord Syntactic { get; }

            public ITypeSymbol ScalarQuantity { get; set; } = null!;
            public OneOf<None, bool> BiasTerm { get; set; }

            public UnitRecord(SyntacticUnitRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticUnitRecord IUnitRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticUnitRecord : ASyntacticRecord, ISyntacticUnitRecord
        {
            public ExpressionSyntax ScalarQuantity { get; set; } = null!;
            public OneOf<None, ExpressionSyntax> BiasTerm { get; set; }

            public SyntacticUnitRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
