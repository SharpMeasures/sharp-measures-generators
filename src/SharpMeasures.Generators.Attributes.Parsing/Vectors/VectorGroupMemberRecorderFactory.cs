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

/// <inheritdoc cref="IVectorGroupMemberRecorderFactory"/>
public sealed class VectorGroupMemberRecorderFactory : IVectorGroupMemberRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IVectorGroupMemberRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="VectorGroupMemberRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorGroupMemberAttribute{TGroup}"/> to recorders.</param>
    public VectorGroupMemberRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IVectorGroupMemberRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IVectorGroupMemberRecord> IVectorGroupMemberRecorderFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new VectorGroupMemberRecordBuilder(attributeSyntax);

        return Factory.Create<IVectorGroupMemberRecord, IVectorGroupMemberRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorGroupMemberRecordBuilder : ARecordBuilder<IVectorGroupMemberRecord>, IVectorGroupMemberRecordBuilder
    {
        private VectorGroupMemberRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public VectorGroupMemberRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticVectorGroupMemberRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IVectorGroupMemberRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Group;

        void IVectorGroupMemberRecordBuilder.WithGroup(ITypeSymbol group, ExpressionSyntax syntax)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Group = group;
            Target.Syntactic.Group = syntax;
            Tracker = Tracker.WithGroup();
        }

        void IVectorGroupMemberRecordBuilder.WithDimension(int dimension, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Dimension = dimension;
            Target.Syntactic.Dimension = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool Group { get; private init; }

            public BuildTracker WithGroup() => this with { Group = true };
        }

        private sealed class VectorGroupMemberRecord : IVectorGroupMemberRecord
        {
            public SyntacticVectorGroupMemberRecord Syntactic { get; }

            public ITypeSymbol Group { get; set; } = null!;
            public OneOf<None, int> Dimension { get; set; }

            public VectorGroupMemberRecord(SyntacticVectorGroupMemberRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticVectorGroupMemberRecord IVectorGroupMemberRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticVectorGroupMemberRecord : ASyntacticRecord, ISyntacticVectorGroupMemberRecord
        {
            public ExpressionSyntax Group { get; set; } = null!;
            public OneOf<None, ExpressionSyntax> Dimension { get; set; }

            public SyntacticVectorGroupMemberRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
