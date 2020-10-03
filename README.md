# AwsLambdaTemplate


AWS Lambda 용 .net core 3.1용 런타임을 사용한 예제


## 기본 런타임 구성
- API Gateway 
- Lambda 
- Cloudwatch Logs(+ insight)


## 설계 특징
- 세팅이 단순하다.
- 전체적인 과금 정책이 실행 동작만큼만 발생한다.
- 사용량이 늘어났을 때 프로비져닝등 별도의 작업을 해주지 않아도 처리 유닛이 알아서 확장된다.
- Timezone, Debugging 등이 환경 변수를 통해서 조절되는 내용들이 있으니 문서를 참조해야 한다.
 (https://docs.aws.amazon.com/lambda/latest/dg/configuration-envvars.html)
- 별도의 logging agent 없이 콘솔출력으로 개행문자만 '\n' 에서 '\r'로 바꿔주면 된다. ( Console.WriteLine( text.Replace('\n','\r')); )

## .Net Core + Lambda 특징
- API 실행 성능이 매우 좋다. (메모리를 512MB만 할당해도 100ms 아래로 대부분 기능들을 수행한다.)
- ReadyToRun 설정을 통해 ColdStart 시간을 큰폭으로 줄일 수 있다.
- Web API 의 템플릿을 공식지원하여 .Net Core Web API와 완전히 똑같이 개발하면 된다.
- LocalEntryPoint / LambdaEntryPoint를 구분하여 초기 동작을 지원한다. (Lambda 환경이 아니어도 실행할 수 있다.)

