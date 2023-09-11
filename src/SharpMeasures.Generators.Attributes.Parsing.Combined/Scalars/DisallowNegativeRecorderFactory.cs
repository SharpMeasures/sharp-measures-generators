namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Scalars;

using System;

/// <inheritdoc cref="IDisallowNegativeRecorderFactory"/>
public sealed class DisallowNegativeRecorderFactory : IDisallowNegativeRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IDisallowNegativeRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="DisallowNegativeRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="DisallowNegativeAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="DisallowNegativeAttribute"/> to recorders.</param>
    public DisallowNegativeRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IDisallowNegativeRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IDisallowNegativeRecord> IRecorderFactory<IDisallowNegativeRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new DisallowNegativeRecordBuilder(attributeSyntax);

        return Factory.Create<IDisallowNegativeRecord, IDisallowNegativeRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class DisallowNegativeRecordBuilder : ARecordBuilder<IDisallowNegativeRecord>, IDisallowNegativeRecordBuilder
    {
        private DisallowNegativeRecord Target { get; }

        public DisallowNegativeRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticDisallowNegativeRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IDisallowNegativeRecord GetRecord() => Target;

        void IDisallowNegativeRecordBuilder.WithBehaviour(DisallowNegativeBehaviour behaviour, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Behaviour = behaviour;
            Target.Syntactic.Behaviour = syntax;
        }

        private sealed class DisallowNegativeRecord : IDisallowNegativeRecord
        {
            public SyntacticDisallowNegativeRecord Syntactic { get; }

            public OneOf<None, DisallowNegativeBehaviour> Behaviour { get; set; }

            public DisallowNegativeRecord(SyntacticDisallowNegativeRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticDisallowNegativeRecord IDisallowNegativeRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticDisallowNegativeRecord : ASyntacticRecord, ISyntacticDisallowNegativeRecord
        {
            public OneOf<None, ExpressionSyntax> Behaviour { get; set; }

            public SyntacticDisallowNegativeRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
