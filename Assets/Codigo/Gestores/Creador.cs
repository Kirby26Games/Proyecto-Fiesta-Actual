using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;


[EditorTool("Creador", typeof(EditorTool))]
public class Creador : EditorWindow
{
    public GameObject Objeto; //Objeto A Crear
    public Transform Padre; //Padre del objeto
    public float TamañoMaximo=1, TamañoMinimo=1; //Tamaño minimo y maximo
    public float RangoX, RangoY,RangoZ;//Variacion de rotacion
    public float Distancia;//Distancia entre objetos
    public int Cantidad;//Objetos que crearemos
    public float Altura; //Cambio en altura al suelo
    public bool Activado; //Si la herramienta esta activa


    [MenuItem("Tools/Colocar")]
    public static void MostrarVentana()
    {
        GetWindow<Creador>();
    }

    private void OnGUI()
    {
        //Establecemos el minimo de tamaño de la ventana
        minSize = new Vector2(300, 200);

        GUILayout.Label("Opciones");
        //Objeto a crear
        EditorGUILayout.BeginHorizontal();
        Objeto = (GameObject)EditorGUILayout.ObjectField("Objeto", Objeto, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();
        //Padre del objeto
        EditorGUILayout.BeginHorizontal();
        Padre = (Transform)EditorGUILayout.ObjectField("Padre", Padre, typeof(Transform), true);
        EditorGUILayout.EndHorizontal();
        //Tamaño minimo y maximo
        TamañoMinimo = EditorGUILayout.FloatField("Tamaño Minimo", TamañoMinimo);
        TamañoMaximo = EditorGUILayout.FloatField("Tamaño Maximo", TamañoMaximo);

        RangoX = EditorGUILayout.FloatField("Rotacion X", RangoX);
        RangoY = EditorGUILayout.FloatField("Rotacion Y", RangoY);
        RangoZ = EditorGUILayout.FloatField("Rotacion Z", RangoZ);
        //Altura del objeto respecto al suelo
        Altura = EditorGUILayout.FloatField("Altura", Altura);
        //Esta activada la herramienta
        Activado = EditorGUILayout.Toggle("Activado", Activado);
    }
    private void OnEnable()
    {
        SceneView.duringSceneGui += ActualizarEscena;
    }
    void OnDestroy()
    {
        SceneView.duringSceneGui -= ActualizarEscena;
    }
    //Esto es como un Update, pero en la interfaz
    void ActualizarEscena(SceneView escena)
    {
        Event evento = Event.current;

        //Si estoy pulsando click  izquierdo y activado es true
        if (evento.type == EventType.MouseDown && evento.button == 0
            && Activado == true)
        {
            InstanciarObjeto(Objeto);
        }
        //Deselecciono el objeto(Como hemos dado click y estamos en editor, el editor intentara
        //Seleccionar el objeto al que clickamos y eso es molesto)
        if (evento.type == EventType.Used
            && Activado == true)
        {
            //Lo deselecciono
            Selection.activeGameObject = null;
        }
    }

    void InstanciarObjeto(GameObject objeto)
    {
        GameObject NuevoObjeto;
        Ray rayoInterfaz = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        //Lanzamos el rayo con una distancia maxima de 1000
        if(Physics.Raycast(rayoInterfaz,out RaycastHit Datos, 1000))
        {
            //Si el objeto es parte de un prefab o es un prefab
            if (PrefabUtility.IsPartOfAnyPrefab(Objeto) == true)
            {
                //Cojo el objeto original, su referencia
                NuevoObjeto = PrefabUtility.GetCorrespondingObjectFromOriginalSource(Objeto);
                //Instancio ese objeto
                NuevoObjeto = PrefabUtility.InstantiatePrefab(NuevoObjeto) as GameObject;
            }
            else
            {
                //Si no, instancio normal
                NuevoObjeto = Instantiate(Objeto);
            }
            NuevoObjeto.transform.parent = Padre;
            NuevoObjeto.transform.SetPositionAndRotation(
            Datos.point+Vector3.up*Altura, Quaternion.FromToRotation(Vector3.up,Datos.normal));
            //Intentar poner a esta rotacion, la modificacion aleatoria que hagamos
            Quaternion rotacionNueva = Quaternion.Euler(Random.Range(-RangoX,RangoX), Random.Range(-RangoY, RangoY), Random.Range(-RangoZ, RangoZ));
            NuevoObjeto.transform.localRotation *= rotacionNueva;
            //Intentar cambiar el tamaño segun los parametros que hemos incluido
            NuevoObjeto.transform.localScale= Vector3.one*Random.Range(TamañoMinimo,TamañoMaximo);
            Undo.RegisterCreatedObjectUndo(NuevoObjeto, "Objeto creado");
        }
    }
}
