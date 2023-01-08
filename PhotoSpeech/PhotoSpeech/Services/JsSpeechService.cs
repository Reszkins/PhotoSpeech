using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using PhotoSpeech.DataAccess.Enums;
using PhotoSpeech.Options;

namespace PhotoSpeech.Services;

public class JsSpeechService : IAsyncDisposable
{
    private readonly IJSObjectReference _jsModule;

    public JsSpeechService(IJSObjectReference jsModule)
    {
        _jsModule = jsModule;
    }

    public async Task InitAzureSpeechService(IOptions<AzureCognitiveOptions> options, Language language)
    {
        await _jsModule.InvokeVoidAsync(
            "setAzureSpeech",
            options.Value,
            language.GetSpeechToTextLanguageCode());
    }

    public async Task StartRecognitionSpeech()
    {
        try
        {
            await _jsModule.InvokeVoidAsync("startSpeech");
        }
        catch (Exception e)
        {
            return;
        }
    }

    public async Task StopRecognitionSpeech()
    {
        try
        {
            await _jsModule.InvokeVoidAsync("stopSpeech");
        }
        catch (Exception e)
        {
            return;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _jsModule.DisposeAsync();
    }
}