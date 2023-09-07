namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Units;

using System;

/// <inheritdoc cref="IUnitInstanceRecorderFactory"/>
public sealed class UnitInstanceRecorderFactory : IUnitInstanceRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IUnitInstanceRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="UnitInstanceRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="UnitInstanceAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="UnitInstanceAttribute"/> to recorders.</param>
    public UnitInstanceRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IUnitInstanceRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IUnitInstanceRecord> IRecorderFactory<IUnitInstanceRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new UnitInstanceRecordBuilder(attributeSyntax);

        return Factory.Create<IUnitInstanceRecord, IUnitInstanceRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class UnitInstanceRecordBuilder : ARecordBuilder<IUnitInstanceRecord>, IUnitInstanceRecordBuilder
    {
        private UnitInstanceRecord Target { get; }

        public UnitInstanceRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticUnitInstanceRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IUnitInstanceRecord GetRecord() => Target;

        void IUnitInstanceRecordBuilder.WithName(string? name, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Name = name;
            Target.Syntactic.Name = syntax;
        }

        void IUnitInstanceRecordBuilder.WithPluralForm(string? pluralForm, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.PluralForm = pluralForm;
            Target.Syntactic.PluralForm = syntax;
        }

        private sealed class UnitInstanceRecord : IUnitInstanceRecord
        {
            public SyntacticUnitInstanceRecord Syntactic { get; }

            public OneOf<None, string?> Name { get; set; }
            public OneOf<None, string?> PluralForm { get; set; }

            public UnitInstanceRecord(SyntacticUnitInstanceRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticUnitInstanceRecord IUnitInstanceRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticUnitInstanceRecord : ASyntacticRecord, ISyntacticUnitInstanceRecord
        {
            public OneOf<None, ExpressionSyntax> Name { get; set; }
            public OneOf<None, ExpressionSyntax> PluralForm { get; set; }

            public SyntacticUnitInstanceRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
