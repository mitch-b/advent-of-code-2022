using System;
namespace AdventOfCode.Solutions.Models;

public class Node<T>
{
	public readonly List<Node<T>> Children;

	public T Value { get; set; }
	public string Label { get; set; }

	public Node(T value = default, string label = "")
	{
		Value = value;
		Label = label;
        Children = new List<Node<T>>();
    }

	/// <summary>
	/// For example: label 'A/B/C' when searched from Node A should traverse child Node B before returning Node C
	/// </summary>
	/// <param name="label"></param>
	/// <returns></returns>
	public Node<T>? TryGetNode(string label)
	{
		var splitBy = new[] { '/' };
		var labelParts = label.Split(splitBy, StringSplitOptions.TrimEntries)
			.Where(l => !string.IsNullOrWhiteSpace(l));
		var traversedLabels = new List<string>();
		if (label == Label || labelParts.All(string.IsNullOrWhiteSpace))
			return this;
		foreach (var labelPart in labelParts)
        {
            traversedLabels.Add(labelPart);
            foreach (var child in Children)
            {
                if (labelPart.Equals(child.Label, StringComparison.CurrentCultureIgnoreCase))
				{
					if (labelPart == labelParts.Last())
						return child;
					var traversedLabel = string.Join("/", traversedLabels);
					var subLabel = label.Replace($"/{traversedLabel}", "");
                    return child.TryGetNode(subLabel);
				}
            }
        }
		return default;
	}
}
