﻿using System;
using SeqCli.PlainText.Parsers;
using Superpower;
using Superpower.Parsers;

namespace SeqCli.PlainText.Patterns
{
    static class ExtractionPatternParser
    {
        static readonly TextParser<LiteralTextPatternExpression> LiteralText =            
            Span.EqualTo("{{").Value('{').Try()            
                .Or(Span.EqualTo("}}").Value('}').Try())
                .Or(Character.ExceptIn('{', '}'))
                .AtLeastOnce()
                .Select(ch => new LiteralTextPatternExpression(new string(ch)));

        static readonly TextParser<string> CaptureName =
            SpanEx.MatchedBy(
                    Character.Letter.Or(Character.In('@', '_'))
                        .IgnoreThen(Character.LetterOrDigit.Or(Character.EqualTo('_')).Many()))
                .Select(s => s.ToStringValue());

        static readonly TextParser<string> CaptureType =
            SpanEx.MatchedBy(Character.EqualTo('*'))
                .Or(SpanEx.MatchedBy(Character.Letter.Or(Character.EqualTo('_'))
                        .IgnoreThen(Character.LetterOrDigit.Or(Character.EqualTo('_')).Many())))
                .Select(s => s.ToStringValue());

        static readonly TextParser<CapturePatternExpression> Capture =
            from _ in Character.EqualTo('{')
            from name in CaptureName.OptionalOrDefault()
            from type in Character.EqualTo(':')
                .IgnoreThen(CaptureType)
                .OptionalOrDefault()
            where name != null || type != null
            from __ in Character.EqualTo('}')
            select new CapturePatternExpression(name, type);

        static readonly TextParser<ExtractionPatternExpression> Element =
            LiteralText.Cast<LiteralTextPatternExpression, ExtractionPatternExpression>()
                .Or(Capture.Cast<CapturePatternExpression, ExtractionPatternExpression>());

        static readonly TextParser<ExtractionPattern> Pattern =
            Element.AtLeastOnce().AtEnd().Select(e => new ExtractionPattern(e));
                    
        public static ExtractionPattern Parse(string extractionPattern)
        {
            if (extractionPattern == null) throw new ArgumentNullException(nameof(extractionPattern));
            if (extractionPattern == "") throw new ParseException("Zero-length extraction patterns are not allowed.");
            return Pattern.Parse(extractionPattern);
        }
    }
}