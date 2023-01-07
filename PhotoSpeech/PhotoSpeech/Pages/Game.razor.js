let SpeechSDK;
let dotNetRef;
let recognizer;

if (!!window.SpeechSDK) {
    SpeechSDK = window.SpeechSDK;
}

export function setDotNetRef(dotNetRefObject) {
    dotNetRef = dotNetRefObject;
}

export function setAzureSpeech(azureCognitiveOptions, language) {
    const speechConfig = SpeechSDK.SpeechConfig.fromSubscription(
        azureCognitiveOptions.key,
        azureCognitiveOptions.location);
    
    speechConfig.speechRecognitionLanguage = language;

    const audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
    recognizer = new SpeechSDK.SpeechRecognizer(speechConfig, audioConfig);

    recognizer.recognizing = async (s, e) => {
        console.log(e.result.text)
        await dotNetRef.invokeMethodAsync('CheckWord', e.result.text);
    };
}

export function startSpeech() {
    recognizer.startContinuousRecognitionAsync();
}

export function stopSpeech() {
    recognizer.stopContinuousRecognitionAsync();
}
