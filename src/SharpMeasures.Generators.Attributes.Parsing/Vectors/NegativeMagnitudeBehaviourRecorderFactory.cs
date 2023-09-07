namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="INegativeMagnitudeBehaviourRecorderFactory"/>
public sealed class NegativeMagnitudeBehaviourRecorderFactory : INegativeMagnitudeBehaviourRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<INegativeMagnitudeBehaviourRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="NegativeMagnitudeBehaviourRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="NegativeMagnitudeBehaviourAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="NegativeMagnitudeBehaviourAttribute"/> to recorders.</param>
    public NegativeMagnitudeBehaviourRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<INegativeMagnitudeBehaviourRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<INegativeMagnitudeBehaviourRecord> IRecorderFactory<INegativeMagnitudeBehaviourRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new NegativeMagnitudeBehaviourRecordBuilder(attributeSyntax);

        return Factory.Create<INegativeMagnitudeBehaviourRecord, INegativeMagnitudeBehaviourRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class NegativeMagnitudeBehaviourRecordBuilder : ARecordBuilder<INegativeMagnitudeBehaviourRecord>, INegativeMagnitudeBehaviourRecordBuilder
    {
        private NegativeMagnitudeBehaviourRecord Target { get; }

        public NegativeMagnitudeBehaviourRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticNegativeMagnitudeBehaviourRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override INegativeMagnitudeBehaviourRecord GetRecord() => Target;

        void INegativeMagnitudeBehaviourRecordBuilder.WithBehaviour(DisallowNegativeBehaviour behaviour, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Behaviour = behaviour;
            Target.Syntactic.Behaviour = syntax;
        }

        private sealed class NegativeMagnitudeBehaviourRecord : INegativeMagnitudeBehaviourRecord
        {
            public SyntacticNegativeMagnitudeBehaviourRecord Syntactic { get; }

            public OneOf<None, DisallowNegativeBehaviour> Behaviour { get; set; }

            public NegativeMagnitudeBehaviourRecord(SyntacticNegativeMagnitudeBehaviourRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticNegativeMagnitudeBehaviourRecord INegativeMagnitudeBehaviourRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticNegativeMagnitudeBehaviourRecord : ASyntacticRecord, ISyntacticNegativeMagnitudeBehaviourRecord
        {
            public OneOf<None, ExpressionSyntax> Behaviour { get; set; }

            public SyntacticNegativeMagnitudeBehaviourRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
