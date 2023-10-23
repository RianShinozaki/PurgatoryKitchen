shader_type spatial;

render_mode unshaded;
uniform sampler2D screenTexture : hint_screen_texture, filter_nearest, repeat_disable;

uniform vec2 screenSize;
uniform vec2 voronoiSize;
uniform float voronoiDisorder;
uniform vec2 timeScale;

uniform float colorSmoothDir;
uniform float colorSmoothSamples;
uniform float colorSmoothRadius;

float hash(vec2 i){
	return fract(sin(i.x * 263.21 + i.y * 302.87)*412.75)*voronoiDisorder;
}

vec3 smoothColor(vec2 pos, vec2 gridSize){
	
	float pi = 6.2831;
	
	vec3 col = texture(screenTexture, pos).xyz;
	
	if (colorSmoothDir < 1. || colorSmoothSamples < 1.)
		return col;
	
	for (float d = 0.; d <= pi; d += pi / colorSmoothDir) {
		for (float i = 1. / colorSmoothSamples; i <= 1.; i += 1. / colorSmoothSamples) {
			col += texture(screenTexture, pos + vec2(cos(d), sin(d)) * i * colorSmoothRadius * gridSize).xyz;
		}
	}
	
	return col / (colorSmoothDir*colorSmoothSamples+1.);
}

void fragment() {
	vec2 sPos, gridSize = 1./screenSize, p;
	float s = 1., c;
	

	for (float x = -round(voronoiSize.x); x < round(voronoiSize.x)+1.; x++)
	{
		for (float y = -round(voronoiSize.y); y < round(voronoiSize.y)+1.; y++){
			p = (round(SCREEN_UV*screenSize) + vec2(x,y))*gridSize;
			p += sin(hash(p)+TIME*timeScale)*gridSize*voronoiSize;
			
			c = distance(SCREEN_UV.xy, p);
			if (c < s){
				s = c;
				sPos = p;
			}
		}
	}
	
	ALBEDO = smoothColor(sPos, gridSize);
	//texture(screenTexture, sPos).xyz;
	
	// Place fragment code here.
}
