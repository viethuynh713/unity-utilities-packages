# Hệ Thống Lưu Trữ và Tải Dữ Liệu (SaveLoadSystem)

## Tổng Quan

Hệ thống SaveLoadSystem là một module cung cấp giao diện thống nhất để lưu trữ và tải dữ liệu trong Unity. Module này hỗ trợ lưu trữ bất kỳ loại dữ liệu nào có thể serialize thông qua các phương thức bất đồng bộ (async/await) sử dụng UniTask.

## Kiến Trúc

### 1. Interface Chính: `ISaveLoadSystem`

```csharp
public interface ISaveLoadSystem
{
    string CustomKey { set; }
    UniTask<SaveLoadResult> SaveData<T>(string key, T value);
    UniTask<T> GetData<T>(string key);
    void ClearData(string key);
    void ClearAllData();
}
```

#### Thuộc Tính:
- **`CustomKey`**: Khóa tùy chỉnh để mã hóa data hoặc tùy chỉnh khóa trước khi lưu. Mặc định không mã hóa/tùy chỉnh key

#### Phương Thức:
- **`SaveData<T>(string key, T value)`**: Lưu dữ liệu bất đồng bộ
- **`GetData<T>(string key)`**: Tải dữ liệu bất đồng bộ
- **`ClearData(string key)`**: Xóa dữ liệu theo key cụ thể
- **`ClearAllData()`**: Xóa toàn bộ dữ liệu

### 2. Enum Kết Quả: `SaveLoadResult`

```csharp
public enum SaveLoadResult
{
    Success,    // Thành công
    Fail,       // Thất bại
}
```

### 3. Implementation: `PlayerPrefSaveLoad`

Lớp implementation chính sử dụng Unity PlayerPrefs để lưu trữ dữ liệu:

```csharp
public class PlayerPrefSaveLoad : ISaveLoadSystem
{
    private string _privateKey;
    
    public string CustomKey
    {
        set => _privateKey = value;
    }
    
    private string HashKey(string key) => _privateKey + key;
    
    // Các phương thức implementation...
}
```

## Tính Năng

### 1. Hỗ Trợ Đa Loại Dữ Liệu

Module có thể xử lý:
- **Kiểu dữ liệu cơ bản**: `int`, `float`, `bool`, `string`
- **Đối tượng phức tạp**: Sử dụng JSON serialization với `JsonUtility`

### 2. Bất Đồng Bộ (Async/Await)

Tất cả các phương thức lưu/tải đều sử dụng `UniTask` để hỗ trợ xử lý bất đồng bộ, không làm block UI.

### 3. Bảo Mật Dữ Liệu

- Sử dụng custom key để hash các key lưu trữ
- Hỗ trợ phân biệt dữ liệu theo từng người dùng

### 4. Xử Lý Lỗi

- Tự động catch và log các lỗi
- Trả về giá trị mặc định khi không tìm thấy dữ liệu
- Trả về `SaveLoadResult` để báo cáo trạng thái

## Cách Sử Dụng

### 1. Khởi Tạo Hệ Thống

```csharp
// Tạo instance
ISaveLoadSystem saveLoadSystem = new PlayerPrefSaveLoad();

// Thiết lập custom key (thường là Player ID)
saveLoadSystem.CustomKey = "Player123";
```

### 2. Lưu Dữ Liệu

```csharp
// Lưu dữ liệu cơ bản
var intResult = await saveLoadSystem.SaveData("score", 1000);
var stringResult = await saveLoadSystem.SaveData("playerName", "John Doe");

// Lưu đối tượng phức tạp
var playerData = new PlayerData
{
    Name = "John",
    Level = 5,
    Items = new List<string> {"sword", "shield"}
};
var objectResult = await saveLoadSystem.SaveData("playerData", playerData);

// Kiểm tra kết quả
if (objectResult == SaveLoadResult.Success)
{
    Debug.Log("Lưu dữ liệu thành công!");
}
```

### 3. Tải Dữ Liệu

```csharp
// Tải dữ liệu cơ bản
int score = await saveLoadSystem.GetData<int>("score");
string playerName = await saveLoadSystem.GetData<string>("playerName");

// Tải đối tượng phức tạp
PlayerData loadedData = await saveLoadSystem.GetData<PlayerData>("playerData");

if (loadedData != null)
{
    Debug.Log($"Tải dữ liệu thành công: {loadedData.Name}");
}
else
{
    Debug.Log("Không tìm thấy dữ liệu, sử dụng giá trị mặc định");
}
```

### 4. Xóa Dữ Liệu

```csharp
// Xóa dữ liệu cụ thể
saveLoadSystem.ClearData("score");

// Xóa toàn bộ dữ liệu
saveLoadSystem.ClearAllData();
```

## Tích Hợp Với VContainer

### 1. Đăng Ký Trong LifetimeScope

```csharp
protected override void Configure(IContainerBuilder builder)
{
    // Đăng ký ISaveLoadSystem
    builder.Register<SaveLoadManager>(Lifetime.Singleton).AsImplementedInterfaces();
}
```

### 2. Dependency Injection

```csharp
public class GameManager
{
    private readonly ISaveLoadSystem _saveLoadSystem;
    
    [Inject]
    public GameManager(ISaveLoadSystem saveLoadSystem)
    {
        _saveLoadSystem = saveLoadSystem;
    }
    
    public async void SaveGame()
    {
        var result = await _saveLoadSystem.SaveData("gameState", currentGameState);
        // Xử lý kết quả...
    }
}
```


## Best Practices

### 1. Xử Lý Lỗi

```csharp
public async Task<bool> SaveDataSafely<T>(string key, T data)
{
    try
    {
        var result = await _saveLoadSystem.SaveData(key, data);
        return result == SaveLoadResult.Success;
    }
    catch (Exception ex)
    {
        Debug.LogError($"Lỗi khi lưu dữ liệu {key}: {ex.Message}");
        return false;
    }
}
```

### 2. Sử dụng Constants cho Keys

```csharp
public static class PlayerPrefConst
{
    public const string MERGE_GAME_DATA = "MergeGameData";
    public const string PLAYER_PROGRESS = "PlayerProgress";
    public const string SETTINGS = "GameSettings";
}
```

### 3. Validation Dữ Liệu

```csharp
public async void LoadDataWithValidation()
{
    var data = await _saveLoadSystem.GetData<PlayerData>("playerData");
    
    if (data != null && IsValidPlayerData(data))
    {
        // Sử dụng dữ liệu
        ApplyPlayerData(data);
    }
    else
    {
        // Tạo dữ liệu mặc định
        CreateDefaultPlayerData();
    }
}

private bool IsValidPlayerData(PlayerData data)
{
    return !string.IsNullOrEmpty(data.Name) && 
           data.Level > 0 && 
           data.Items != null;
}
```

## Lưu Ý Quan Trọng

### 1. Hiệu Suất

- PlayerPrefs có giới hạn về kích thước dữ liệu
- Đối với dữ liệu lớn, nên xem xét sử dụng file system
- Tránh lưu/tải quá thường xuyên

### 2. Bảo Mật

- PlayerPrefs không được mã hóa mặc định
- Dữ liệu nhạy cảm nên được mã hóa trước khi lưu
- Custom key giúp tăng tính bảo mật cơ bản

### 3. Platform Compatibility

- PlayerPrefs hoạt động trên tất cả platform Unity hỗ trợ
- Dữ liệu được lưu ở registry (Windows), plist (Mac), preferences (Linux)



## Troubleshooting

### Lỗi Thường Gặp

1. **Serialization Error**: Đảm bảo class có thể serialize
2. **Key Not Found**: Kiểm tra key name và custom key
3. **Null Data**: Xử lý trường hợp dữ liệu không tồn tại
4. **Performance Issues**: Tránh lưu dữ liệu quá lớn hoặc quá thường xuyên

## TODO

1. **Thêm Encryption**: Mã hóa dữ liệu trước khi lưu
2. **File-based Storage**: Tạo implementation mới sử dụng file system
3. **Cloud Save**: Tích hợp với các dịch vụ cloud storage
4. **Compression**: Nén dữ liệu để tiết kiệm dung lượng
5. **Versioning**: Hỗ trợ phiên bản dữ liệu để migration


