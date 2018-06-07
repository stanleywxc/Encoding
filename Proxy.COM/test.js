try {
	var objEnc = new ActiveXObject( "Encoding.Idn.1" );
	WScript.Echo( "Type: " + typeof( objEnc ) );

	if( null == objEnc )
		WScript.Echo( "Failed to create the object." );

	var race = objEnc.PunycodeToRace("http://localhost/WebServices/Idn/Converter.asmx", "xn--cjr6vy5ejyai80u.com");
	WScript.Echo("race domain:" + race);

}catch(e) {
	WScript.Echo( "Caught Exception." );
}