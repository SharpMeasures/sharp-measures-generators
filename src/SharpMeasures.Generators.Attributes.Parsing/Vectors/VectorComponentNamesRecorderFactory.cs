namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Vectors;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="IVectorComponentNamesRecorderFactory"/>
public sealed class VectorComponentNamesRecorderFactory : IVectorComponentNamesRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IVectorComponentNamesRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="VectorComponentNamesRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="VectorComponentNamesAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorComponentNamesAttribute"/> to recorders.</param>
    public VectorComponentNamesRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IVectorComponentNamesRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IVectorComponentNamesRecord> IVectorComponentNamesRecorderFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new VectorComponentNamesRecordBuilder(attributeSyntax);

        return Factory.Create<IVectorComponentNamesRecord, IVectorComponentNamesRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorComponentNamesRecordBuilder : ARecordBuilder<IVectorComponentNamesRecord>, IVectorComponentNamesRecordBuilder
    {
        private VectorComponentNamesRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public VectorComponentNamesRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticVectorComponentNamesRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IVectorComponentNamesRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Names || Tracker.Expression;

        void IVectorComponentNamesRecordBuilder.WithNames(IReadOnlyList<string?>? names, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Names = OneOf<None, IReadOnlyList<string?>?>.FromT1(names);
            Target.Syntactic.Names = syntax;
            Tracker = Tracker.WithNames();
        }

        void IVectorComponentNamesRecordBuilder.WithExpression(string? expression, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Expression = expression;
            Target.Syntactic.Expression = syntax;
            Tracker = Tracker.WithExpression();
        }

        private readonly struct BuildTracker
        {
            public bool Names { get; private init; }
            public bool Expression { get; private init; }

            public BuildTracker WithNames() => this with { Names = true };
            public BuildTracker WithExpression() => this with { Expression = true };
        }

        private sealed class VectorComponentNamesRecord : IVectorComponentNamesRecord
        {
            public SyntacticVectorComponentNamesRecord Syntactic { get; }

            public OneOf<None, IReadOnlyList<string?>?> Names { get; set; }
            public OneOf<None, string?> Expression { get; set; }

            public VectorComponentNamesRecord(SyntacticVectorComponentNamesRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticVectorComponentNamesRecord IVectorComponentNamesRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticVectorComponentNamesRecord : ASyntacticRecord, ISyntacticVectorComponentNamesRecord
        {
            public OneOf<None, ExpressionSyntax> Names { get; set; }
            public OneOf<None, ExpressionSyntax> Expression { get; set; }

            public SyntacticVectorComponentNamesRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
