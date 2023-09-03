namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="IScalarAssociationRecorderFactory"/>
public sealed class ScalarAssociationRecorderFactory : IScalarAssociationRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IScalarAssociationRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="ScalarAssociationRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="ScalarAssociationAttribute{TScalar}"/> to recorders.</param>
    public ScalarAssociationRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IScalarAssociationRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IScalarAssociationRecord> IScalarAssociationRecorderFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new ScalarAssociationRecordBuilder(attributeSyntax);

        return Factory.Create<IScalarAssociationRecord, IScalarAssociationRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class ScalarAssociationRecordBuilder : ARecordBuilder<IScalarAssociationRecord>, IScalarAssociationRecordBuilder
    {
        private ScalarAssociationRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public ScalarAssociationRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticScalarAssociationRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IScalarAssociationRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.ScalarQuantity;

        void IScalarAssociationRecordBuilder.WithScalarQuantity(ITypeSymbol scalarQuantity, ExpressionSyntax syntax)
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

        void IScalarAssociationRecordBuilder.WithAsComponents(bool asComponents, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.AsComponents = asComponents;
            Target.Syntactic.AsComponents = syntax;
        }

        void IScalarAssociationRecordBuilder.WithAsMagnitude(bool asMagnitude, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.AsMagnitude = asMagnitude;
            Target.Syntactic.AsMagnitude = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool ScalarQuantity { get; private init; }

            public BuildTracker WithScalarQuantity() => this with { ScalarQuantity = true };
        }

        private sealed class ScalarAssociationRecord : IScalarAssociationRecord
        {
            public SyntacticScalarAssociationRecord Syntactic { get; }

            public ITypeSymbol ScalarQuantity { get; set; } = null!;
            public OneOf<None, bool> AsComponents { get; set; }
            public OneOf<None, bool> AsMagnitude { get; set; }

            public ScalarAssociationRecord(SyntacticScalarAssociationRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticScalarAssociationRecord IScalarAssociationRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticScalarAssociationRecord : ASyntacticRecord, ISyntacticScalarAssociationRecord
        {
            public ExpressionSyntax ScalarQuantity { get; set; } = null!;
            public OneOf<None, ExpressionSyntax> AsComponents { get; set; }
            public OneOf<None, ExpressionSyntax> AsMagnitude { get; set; }

            public SyntacticScalarAssociationRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
