using Microsoft.AspNetCore.Mvc;
using taniTenkan.Models;

namespace taniTenkan.Controllers;

public class taniTenkanController : Controller {
    private static readonly Dictionary<string, double> areaKankei = new() {
        {"square_millimeter", 0.000001},
        {"square_centimeter", 0.0001},
        {"square_meter", 1},
        {"square_kilometer", 1000000},
        {"square_inch", 0.00064516},
        {"square_foot", 0.092903},
        {"square_yard", 0.836127},
        {"acre", 4046.86},
        {"hectare", 10000}
    };

    private static readonly Dictionary<string, double> lengthKankei = new() {
        {"millimeter", 0.001},
        {"centimeter", 0.01},
        {"meter", 1},
        {"kilometer", 1000},
        {"inch", 0.0254},
        {"foot", 0.3048},
        {"yard", 0.9144},
        {"mile", 1609.34}
    };

    private static readonly Dictionary<string, double> volumeKankei = new() {
        {"milliliter", 0.001},
        {"liter", 1},
        {"cubic_meter", 1000},
        {"gallon", 3.78541},
        {"quart", 0.946353},
        {"pint", 0.473176},
        {"cup", 0.24},
        {"fluid_ounce", 0.0295735}
    };

    private static readonly Dictionary<string, double> weightKankei = new() {
        {"milligram", 0.001},
        {"gram", 1},
        {"kilogram", 1000},
        {"ounce", 28.3495},
        {"pound", 453.592}
    };

    public IActionResult Area() {
        return View(new Conversion {Type = Syurui.Area});
    }

    public IActionResult Length() {
        return View(new Conversion {Type = Syurui.Length});
    }

    public IActionResult Temperature() {
        return View(new Conversion {Type = Syurui.Temperature});
    }

    public IActionResult Volume() {
        return View(new Conversion {Type = Syurui.Volume});
    }

    public IActionResult Weight() {
        return View(new Conversion {Type = Syurui.Weight});
    }

    [HttpPost]
    public IActionResult Convert(Conversion model) {
        if (ModelState.IsValid)
            model.OutputValue = ConvertUnits(model.InputValue, model.FromUnit, model.ToUnit, model.Type);

        return View(model.Type.ToString(), model);
    }

    public double? ConvertUnits(double InputValue, string FromUnit, string ToUnit, Syurui type) {
        switch (type) {
            case Syurui.Area:
                return ConvertArea(InputValue, FromUnit, ToUnit);
            case Syurui.Length:
                return ConvertLength(InputValue, FromUnit, ToUnit);
            case Syurui.Temperature:
                return ConvertTemperature(InputValue, FromUnit, ToUnit);
            case Syurui.Volume:
                return ConvertVolume(InputValue, FromUnit, ToUnit);
            case Syurui.Weight:
                return ConvertWeight(InputValue, FromUnit, ToUnit);
            default:
                return null;
        }
    }

    public double? ConvertArea(double InputValue, string FromUnit, string ToUnit) {
        if (areaKankei.TryGetValue(FromUnit, out double fromFactor) && areaKankei.TryGetValue(ToUnit, out double toFactor)) {
            double middleValue = InputValue * fromFactor;
            return middleValue / toFactor;
        }

        return null;
    }

    public double? ConvertLength(double InputValue, string FromUnit, string ToUnit) {
        if (lengthKankei.TryGetValue(FromUnit, out double fromFactor) && lengthKankei.TryGetValue(ToUnit, out double toFactor)) {
            double middleValue = InputValue * fromFactor;
            return middleValue / toFactor;
        }

        return null;
    }

    public double? ConvertVolume(double InputValue, string FromUnit, string ToUnit) {
        if (volumeKankei.TryGetValue(FromUnit, out double fromFactor) && volumeKankei.TryGetValue(ToUnit, out double toFactor)) {
            double middleValue = InputValue * fromFactor;
            return middleValue / toFactor;
        }

        return null;
    }

    public double? ConvertWeight(double InputValue, string FromUnit, string ToUnit) {
        if (weightKankei.TryGetValue(FromUnit, out double fromFactor) && weightKankei.TryGetValue(ToUnit, out double toFactor)) {
            double middleValue = InputValue * fromFactor;
            return middleValue / toFactor;
        }

        return null;
    }

    public double? ConvertTemperature(double InputValue, string FromUnit, string ToUnit) {
        if (FromUnit == ToUnit)
            return InputValue;

        double valueInCelsius;

        switch (FromUnit) {
            case "Celsius":
                valueInCelsius = InputValue;
                break;
            case "Fahrenheit":
                valueInCelsius = (InputValue - 32) * 5 / 9;
                break;
            case "Kelvin":
                valueInCelsius = InputValue - 273.15;
                break;
            default:
                return null;
        }

        switch (ToUnit) {
            case "Celsius":
                return valueInCelsius;
            case "Fahrenheit":
                return valueInCelsius * 9 / 5 + 32;
            case "Kelvin":
                return valueInCelsius + 273.15;
            default:
                return null;
        }
    }
} 