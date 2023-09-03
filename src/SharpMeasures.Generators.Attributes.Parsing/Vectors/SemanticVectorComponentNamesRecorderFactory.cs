namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures;
using SharpMeasures.Generators.Attributes.Vectors;

using System;
using System.Collections.Generic;

/// <inheritdoc cref="ISemanticVectorComponentNamesRecorderFactory"/>
public sealed class SemanticVectorComponentNamesRecorderFactory : ISemanticVectorComponentNamesRecorderFactory
{
    private ISemanticRecorderFactory Factory { get; }
    private ISemanticMapper<ISemanticVectorComponentNamesRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="SemanticVectorComponentNamesRecorderFactory"/>, handling creation of <see cref="ISemanticRecorder{TRecord}"/> for recording the arguments of <see cref="VectorComponentNamesAttribute"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ISemanticRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="VectorComponentNamesAttribute"/> to recorders.</param>
    public SemanticVectorComponentNamesRecorderFactory(ISemanticRecorderFactory factory, ISemanticMapper<ISemanticVectorComponentNamesRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ISemanticRecorder<ISemanticVectorComponentNamesRecord> ISemanticRecorderFactory<ISemanticVectorComponentNamesRecord>.Create()
    {
        var recordBuilder = new VectorComponentNamesRecordBuilder();

        return Factory.Create<ISemanticVectorComponentNamesRecord, ISemanticVectorComponentNamesRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class VectorComponentNamesRecordBuilder : ARecordBuilder<ISemanticVectorComponentNamesRecord>, ISemanticVectorComponentNamesRecordBuilder
    {
        private VectorComponentNamesRecord Target { get; } = new();
        private BuildTracker Tracker { get; set; } = new();

        public VectorComponentNamesRecordBuilder() : base(throwOnMultipleBuilds: true) { }

        protected override ISemanticVectorComponentNamesRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Names || Tracker.Expression;

        void ISemanticVectorComponentNamesRecordBuilder.WithNames(IReadOnlyList<string?>? names)
        {
            VerifyCanModify();

            Target.Names = OneOf<None, IReadOnlyList<string?>?>.FromT1(names);
            Tracker = Tracker.WithNames();
        }

        void ISemanticVectorComponentNamesRecordBuilder.WithExpression(string? expression)
        {
            VerifyCanModify();

            Target.Expression = expression;
            Tracker = Tracker.WithExpression();
        }

        private readonly struct BuildTracker
        {
            public bool Names { get; private init; }
            public bool Expression { get; private init; }

            public BuildTracker WithNames() => this with { Names = true };
            public BuildTracker WithExpression() => this with { Expression = true };
        }

        private sealed class VectorComponentNamesRecord : ISemanticVectorComponentNamesRecord
        {
            public OneOf<None, IReadOnlyList<string?>?> Names { get; set; }
            public OneOf<None, string?> Expression { get; set; }
        }
    }
}
