using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionalProgrammingInCSharp
{
    public static class FunctionalExtensions
    {
        public static TResult Map<TSource, TResult>(
            this TSource @this,
            Func<TSource, TResult> fn) => fn(@this);

        public static T Tee<T>(this T @this,
            Action<T> act)
        {
            act(@this);
            return @this;
        }
    }

    public static class Disposable
    {
        public static TResult Using<TDisposable, TResult>(
            Func<TDisposable> factory,
            Func<TDisposable, TResult> fn) where TDisposable : IDisposable
        {
            using (var disposabe = factory())
            {
                return fn(disposabe);
            }
        }
    }

    public static class StringbuilderExtensions
    {
        public static StringBuilder AppendFormattedLine(
            this StringBuilder @this,
            string format,
            params object[] args) =>
            @this.AppendFormat(format, args).AppendLine();

        public static StringBuilder AppendWhen(
            this StringBuilder @this,
            Func<bool> predicate,
            Func<StringBuilder, StringBuilder> fn) =>
            predicate() ?
            fn(@this)
            : @this;

        public static StringBuilder AppendSequence<T>(
            this StringBuilder @this,
            IEnumerable<T> seq,
            Func<StringBuilder, T, StringBuilder> fn) =>
            seq.Aggregate(@this, fn);
    }

    internal class Program
    {
        private static Func<IDictionary<int, string>, string> BuildSelectBox(string id, bool includeunknown)
        =>
            options =>
            new StringBuilder()
                .AppendFormattedLine("<select id=\"{0}\" name = \"{0}\">", id)
                .AppendWhen(
                    () => includeunknown,
                    sb => sb.AppendLine("\t<option>Unknown</option>"))
                .AppendSequence(
                    options,
                    (sb, opt) => sb.AppendFormattedLine("\t<option value=\"{0}\">{1}</option>", opt.Key, opt.Value))
                .AppendLine("</select>")
                .ToString();

        private static void Main(string[] args)
        {
            Disposable.Using(
                StreamFactory.GetStream,
                stream => new byte[stream.Length].Tee(b => stream.Read(b, 0, (int)stream.Length)))
                .Map(Encoding.UTF8.GetString)
                .Split(new[] { Environment.NewLine, }, StringSplitOptions.RemoveEmptyEntries)
                .Select((s, ix) => Tuple.Create(ix + 1, s))
                .ToDictionary(k => k.Item1, v => v.Item2)
                .Map(BuildSelectBox("theDoctors", true))
                .Tee(Console.WriteLine);

            Console.ReadLine();
        }
    }
}