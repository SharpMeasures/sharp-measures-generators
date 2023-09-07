namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Vectors;

using System;

/// <inheritdoc cref="ISemanticVectorGroupMemberRecorderFactory"/>
public sealed class SemanticVectorGroupMemberRecorderFactory : ISemanticVectorGroupMemberRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticVectorGroupMemberRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticVectorGroupMemberRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="VectorGroupMemberAttribute{TGroup}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorGroupMemberAttribute{TGroup}"/> to recorders.</param>
    public SemanticVectorGroupMemberRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticVectorGroupMemberRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticVectorGroupMemberRecord> ISemanticRecorderFactory<ISemanticVectorGroupMemberRecord>.Create()
    {
        var recordBuilder = new VectorGroupMemberRecordBuilder();

        return Factory.Create<ISemanticVectorGroupMemberRecord, ISemanticVectorGroupMemberRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorGroupMemberRecordBuilder : ARecordBuilder<ISemanticVectorGroupMemberRecord>, ISemanticVectorGroupMemberRecordBuilder
    {
        private VectorGroupMemberRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public VectorGroupMemberRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticVectorGroupMemberRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Group;

        void ISemanticVectorGroupMemberRecordBuilder.WithGroup(ITypeSymbol group)
        {
            if (group is null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            VerifyCanModify();

            Target.Group = group;
            Tracker = Tracker.WithGroup();
        }

        void ISemanticVectorGroupMemberRecordBuilder.WithDimension(int dimension)
        {
            VerifyCanModify();

            Target.Dimension = dimension;
        }

        private readonly struct BuildTracker
        {
            public bool Group { get; private init; }

            public BuildTracker WithGroup() => this with { Group = true };
        }

        private sealed class VectorGroupMemberRecord : ISemanticVectorGroupMemberRecord
        {
            public ITypeSymbol Group { get; set; } = null!;
            public OneOf<None, int> Dimension { get; set; }
        }
    }
}
