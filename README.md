# .NET 10 OpenAPI null enum bug

Provides example of null enum bug

## Reproduction:

when `query2` gets registered before `query1` the openapi json adds a `null` value to the `XYZ` enum

```json
"components": {
"schemas": {
    "ABC": {
    "enum": [
        "A",
        "B",
        "C"
    ]
    },
    "XYZ": {
    "enum": [
        "X",
        "Y",
        "Z",
        null
    ]
    }
}

```

however when `query1` is registered before `query2` the null value is not there (expected).

This can also be achieved by simple having the nullable parameters before the non nullable parameters.
