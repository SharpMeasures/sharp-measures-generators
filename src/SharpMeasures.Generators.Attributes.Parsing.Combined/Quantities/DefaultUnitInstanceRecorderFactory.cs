namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="IDefaultUnitInstanceRecorderFactory"/>
public sealed class DefaultUnitInstanceRecorderFactory : IDefaultUnitInstanceRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IDefaultUnitInstanceRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="DefaultUnitInstanceRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="DefaultUnitInstanceAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="DefaultUnitInstanceAttribute"/> to recorders.</param>
    public DefaultUnitInstanceRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IDefaultUnitInstanceRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IDefaultUnitInstanceRecord> IRecorderFactory<IDefaultUnitInstanceRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new DefaultUnitInstanceRecordBuilder(attributeSyntax);

        return Factory.Create<IDefaultUnitInstanceRecord, IDefaultUnitInstanceRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class DefaultUnitInstanceRecordBuilder : ARecordBuilder<IDefaultUnitInstanceRecord>, IDefaultUnitInstanceRecordBuilder
    {
        private DefaultUnitInstanceRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public DefaultUnitInstanceRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticDefaultUnitInstanceRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IDefaultUnitInstanceRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.UnitInstance;

        void IDefaultUnitInstanceRecordBuilder.WithUnitInstance(string? unitInstance, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.UnitInstance = unitInstance;
            Target.Syntactic.UnitInstance = syntax;
            Tracker = Tracker.WithUnitInstance();
        }

        void IDefaultUnitInstanceRecordBuilder.WithSymbol(string? symbol, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Symbol = symbol;
            Target.Syntactic.Symbol = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool UnitInstance { get; private init; }

            public BuildTracker WithUnitInstance() => this with { UnitInstance = true };
        }

        private sealed class DefaultUnitInstanceRecord : IDefaultUnitInstanceRecord
        {
            public SyntacticDefaultUnitInstanceRecord Syntactic { get; }

            public string? UnitInstance { get; set; }
            public OneOf<None, string?> Symbol { get; set; }

            public DefaultUnitInstanceRecord(SyntacticDefaultUnitInstanceRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticDefaultUnitInstanceRecord IDefaultUnitInstanceRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticDefaultUnitInstanceRecord : ASyntacticRecord, ISyntacticDefaultUnitInstanceRecord
        {
            public ExpressionSyntax UnitInstance { get; set; } = null!;
            public OneOf<None, ExpressionSyntax> Symbol { get; set; }

            public SyntacticDefaultUnitInstanceRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
