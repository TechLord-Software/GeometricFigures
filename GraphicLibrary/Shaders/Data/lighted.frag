#version 330

struct Material{
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float shininness;
    float transparency;
};
struct LightSource {
    vec3 position;

    float constant;
    float linear;
    float quadratic;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

const int maxLightSources = 32;

in vec3 Normal;
in vec3 Position;

uniform vec3 cameraPosition;
uniform Material material;
uniform int lightSourceCount;
uniform LightSource lights[maxLightSources];

out vec4 outColor;



vec3 calculateLight(LightSource light, vec3 normal, vec3 position, vec3 viewDirection);

void main()
{
    vec3 normal = normalize(Normal);
    vec3 viewDirection = normalize(cameraPosition - Position);
    vec3 result = vec3(0.0);

    for (int i = 0; i < lightSourceCount; i++)
    {
        result += calculateLight(lights[i], normal, Position, viewDirection);
    }

    outColor = vec4(result, material.transparency);
}

vec3 calculateLight(LightSource light, vec3 normal, vec3 position, vec3 viewDirection){
    vec3 lightDirection = normalize(light.position - position);

    float diff = max(dot(normal, lightDirection), 0.0);

    vec3 reflectDirection = reflect(-lightDirection, normal);
    float spec = pow(max(dot(viewDirection, reflectDirection), 0.0), material.shininness);
    
    float lightDistance = length(light.position - position);
    float attenuation = 1.0 / (light.constant + light.linear * lightDistance + light.quadratic * (lightDistance * lightDistance));

    vec3 ambient = light.ambient * material.ambient;
    vec3 diffuse = light.diffuse * diff * material.diffuse;
    vec3 specular = light.specular * spec * material.specular;

    ambient *= attenuation;
    diffuse *= attenuation;
    specular *= attenuation;

    return ambient + diffuse + specular;
}