namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Units;

using System;

/// <inheritdoc cref="IExtendedUnitRecorderFactory"/>
public sealed class ExtendedUnitRecorderFactory : IExtendedUnitRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IExtendedUnitRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="ExtendedUnitRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="ExtendedUnitAttribute{TOriginal}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="ExtendedUnitAttribute{TOriginal}"/> to recorders.</param>
    public ExtendedUnitRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IExtendedUnitRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IExtendedUnitRecord> IRecorderFactory<IExtendedUnitRecord>.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new ExtendedUnitRecordBuilder(attributeSyntax);

        return Factory.Create<IExtendedUnitRecord, IExtendedUnitRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class ExtendedUnitRecordBuilder : ARecordBuilder<IExtendedUnitRecord>, IExtendedUnitRecordBuilder
    {
        private ExtendedUnitRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public ExtendedUnitRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticExtendedUnitRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IExtendedUnitRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Original;

        void IExtendedUnitRecordBuilder.WithOriginal(ITypeSymbol original, ExpressionSyntax syntax)
        {
            if (original is null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Original = original;
            Target.Syntactic.Original = syntax;
            Tracker = Tracker.WithOriginal();
        }

        private readonly struct BuildTracker
        {
            public bool Original { get; private init; }

            public BuildTracker WithOriginal() => this with { Original = true };
        }

        private sealed class ExtendedUnitRecord : IExtendedUnitRecord
        {
            public SyntacticExtendedUnitRecord Syntactic { get; }

            public ITypeSymbol Original { get; set; } = null!;

            public ExtendedUnitRecord(SyntacticExtendedUnitRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticExtendedUnitRecord IExtendedUnitRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticExtendedUnitRecord : ASyntacticRecord, ISyntacticExtendedUnitRecord
        {
            public ExpressionSyntax Original { get; set; } = null!;

            public SyntacticExtendedUnitRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
