namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using OneOf;
using OneOf.Types;

using SharpAttributeParser;
using SharpAttributeParser.Mappers;

using SharpMeasures.Generators.Attributes.Quantities;

using System;

/// <inheritdoc cref="IQuantityOperationRecorderFactory"/>
public sealed class QuantityOperationRecorderFactory : IQuantityOperationRecorderFactory
{
    private ICombinedRecorderFactory Factory { get; }
    private ICombinedMapper<IQuantityOperationRecordBuilder> Mapper { get; }

    /// <summary>Instantiates a <see cref="QuantityOperationRecorderFactory"/>, handling creation of <see cref="ICombinedRecorder{TRecord}"/> for recording the arguments of <see cref="QuantityOperationAttribute{TResult, TOther}"/>.</summary>
    /// <param name="factory">The factory used to create <see cref="ICombinedRecorder{TRecord}"/>.</param>
    /// <param name="mapper">Provides mappings from the parameters of <see cref="QuantityOperationAttribute{TResult, TOther}"/> to recorders.</param>
    public QuantityOperationRecorderFactory(ICombinedRecorderFactory factory, ICombinedMapper<IQuantityOperationRecordBuilder> mapper)
    {
        Factory = factory ?? throw new ArgumentNullException(nameof(factory));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    ICombinedRecorder<IQuantityOperationRecord> IQuantityOperationRecorderFactory.Create(AttributeSyntax attributeSyntax)
    {
        if (attributeSyntax is null)
        {
            throw new ArgumentNullException(nameof(attributeSyntax));
        }

        var recordBuilder = new QuantityOperationRecordBuilder(attributeSyntax);

        return Factory.Create<IQuantityOperationRecord, IQuantityOperationRecordBuilder>(Mapper, recordBuilder);
    }

    private sealed class QuantityOperationRecordBuilder : ARecordBuilder<IQuantityOperationRecord>, IQuantityOperationRecordBuilder
    {
        private QuantityOperationRecord Target { get; }
        private BuildTracker Tracker { get; set; } = new();

        public QuantityOperationRecordBuilder(AttributeSyntax attributeSyntax) : base(throwOnMultipleBuilds: true)
        {
            SyntacticQuantityOperationRecord syntactic = new(attributeSyntax);

            Target = new(syntactic);
        }

        protected override IQuantityOperationRecord GetRecord() => Target;
        protected override bool CanBuildRecord() => Tracker.Result && Tracker.Other && Tracker.OperatorType;

        void IQuantityOperationRecordBuilder.WithResult(ITypeSymbol result, ExpressionSyntax syntax)
        {
            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Result = result;
            Target.Syntactic.Result = syntax;
            Tracker = Tracker.WithResult();
        }

        void IQuantityOperationRecordBuilder.WithOther(ITypeSymbol other, ExpressionSyntax syntax)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.Other = other;
            Target.Syntactic.Other = syntax;
            Tracker = Tracker.WithOther();
        }

        void IQuantityOperationRecordBuilder.WithOperatorType(OperatorType operatorType, ExpressionSyntax syntax)
        {
            if (syntax is null)
            {
                throw new ArgumentNullException(nameof(syntax));
            }

            VerifyCanModify();

            Target.OperatorType = operatorType;
            Target.Syntactic.OperatorType = syntax;
            Tracker = Tracker.WithOperatorType();
        }

        void IQuantityOperationRecordBuilder.WithPosition(OperationPosition position, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.Position = position;
            Target.Syntactic.Position = syntax;
        }

        void IQuantityOperationRecordBuilder.WithMirrorMode(OperationMirrorMode mirrorMode, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.MirrorMode = mirrorMode;
            Target.Syntactic.MirrorMode = syntax;
        }

        void IQuantityOperationRecordBuilder.WithImplementation(OperationImplementation implementation, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.Implementation = implementation;
            Target.Syntactic.Implementation = syntax;
        }

        void IQuantityOperationRecordBuilder.WithMirroredImplementation(OperationImplementation mirroredImplementation, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.MirroredImplementation = mirroredImplementation;
            Target.Syntactic.MirroredImplementation = syntax;
        }

        void IQuantityOperationRecordBuilder.WithMethodName(string? methodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.MethodName = methodName;
            Target.Syntactic.MethodName = syntax;
        }

        void IQuantityOperationRecordBuilder.WithStaticMethodName(string? staticMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.StaticMethodName = staticMethodName;
            Target.Syntactic.StaticMethodName = syntax;
        }

        void IQuantityOperationRecordBuilder.WithMirroredMethodName(string? mirroredMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.MirroredMethodName = mirroredMethodName;
            Target.Syntactic.MirroredMethodName = syntax;
        }

        void IQuantityOperationRecordBuilder.WithMirroredStaticMethodName(string? mirroredStaticMethodName, OneOf<None, ExpressionSyntax> syntax)
        {
            VerifyOneOfSyntax.Verify(syntax);

            VerifyCanModify();

            Target.MirroredStaticMethodName = mirroredStaticMethodName;
            Target.Syntactic.MirroredStaticMethodName = syntax;
        }

        private readonly struct BuildTracker
        {
            public bool Result { get; private init; }
            public bool Other { get; private init; }
            public bool OperatorType { get; private init; }

            public BuildTracker WithResult() => this with { Result = true };
            public BuildTracker WithOther() => this with { Other = true };
            public BuildTracker WithOperatorType() => this with { OperatorType = true };
        }

        private sealed class QuantityOperationRecord : IQuantityOperationRecord
        {
            public SyntacticQuantityOperationRecord Syntactic { get; }

            public ITypeSymbol Result { get; set; } = null!;
            public ITypeSymbol Other { get; set; } = null!;

            public OperatorType OperatorType { get; set; }
            public OneOf<None, OperationPosition> Position { get; set; }
            public OneOf<None, OperationMirrorMode> MirrorMode { get; set; }
            public OneOf<None, OperationImplementation> Implementation { get; set; }
            public OneOf<None, OperationImplementation> MirroredImplementation { get; set; }

            public OneOf<None, string?> MethodName { get; set; }
            public OneOf<None, string?> StaticMethodName { get; set; }
            public OneOf<None, string?> MirroredMethodName { get; set; }
            public OneOf<None, string?> MirroredStaticMethodName { get; set; }

            public QuantityOperationRecord(SyntacticQuantityOperationRecord syntactic)
            {
                Syntactic = syntactic;
            }

            ISyntacticQuantityOperationRecord IQuantityOperationRecord.Syntactic => Syntactic;
        }

        private sealed class SyntacticQuantityOperationRecord : ASyntacticRecord, ISyntacticQuantityOperationRecord
        {
            public ExpressionSyntax Result { get; set; } = null!;
            public ExpressionSyntax Other { get; set; } = null!;

            public ExpressionSyntax OperatorType { get; set; } = null!;
            public OneOf<None, ExpressionSyntax> Position { get; set; }
            public OneOf<None, ExpressionSyntax> MirrorMode { get; set; }
            public OneOf<None, ExpressionSyntax> Implementation { get; set; }
            public OneOf<None, ExpressionSyntax> MirroredImplementation { get; set; }

            public OneOf<None, ExpressionSyntax> MethodName { get; set; }
            public OneOf<None, ExpressionSyntax> StaticMethodName { get; set; }
            public OneOf<None, ExpressionSyntax> MirroredMethodName { get; set; }
            public OneOf<None, ExpressionSyntax> MirroredStaticMethodName { get; set; }

            public SyntacticQuantityOperationRecord(AttributeSyntax attribute) : base(attribute) { }
        }
    }
}
