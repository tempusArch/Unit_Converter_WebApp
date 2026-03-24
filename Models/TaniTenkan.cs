namespace taniTenkan.Models;

public enum Syurui {
    Area,
    Length,
    Temperature,
    Volume,
    Weight
}

public class Conversion {
    public double InputValue {get; set;}
    public double? OutputValue {get; set;}
    public string FromUnit {get; set;}
    public string ToUnit {get; set;}
    public Syurui Type {get; set;}
}