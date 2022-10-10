using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Referência para o controlador de movimentação
    public CharacterController controller;

    [Header("Atributtes")]
    //Velocidade
    [SerializeField] float speedMovement = 7f;
    //Input horizontal
    float horizontal;
    //Input vertical
    float vertical;

    [Header("Smooth")]
    //Tempo de suavização da rotação do player
    [SerializeField] float turnSmoothTime = 0.1f;
    //Velocidade de suavização do movimento
    float turnSmoothVelocity;

    [Header("Joystick")]
    //Referência para o joystick que será usado
    [SerializeField] Joystick joystick;

    [Header("Animation")]
    //Referência para o controlador de animação
    [SerializeField] Animator animator;
    //Verificador de movimentação
    [SerializeField] float isMoving;

    [Header("Input Type")]
    //Tipo de controle que vai usar
    [SerializeField] InputType inputType;

    // Update is called once per frame
    void Update()
    {
        //Botões
        //Escolhendo o 
        switch(inputType)
        {
            //Se o tipo de Input for PC
            case InputType.PC:
                //Pegue as teclas que representam o input Horizontal e Vertical
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
                break;
            //Se o tipo de Input for Mobile
            case InputType.MOBILE:
                //Pegue a direção na horizontal e vertical do joystick
                horizontal = joystick.Horizontal;
                vertical = joystick.Vertical;
                break;
        }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //Se o botão estiver pressionado
        if(direction.magnitude > 0.1f)
        {
            //Ângulo em radianos dos eixos x e z e transformado em graus
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //Suavizar a angulação
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //Aplicar a direção e velocidade no Character Controller
            controller.Move(direction * speedMovement * Time.deltaTime);
        }
        
        //Aplicar a direção no verificar de movimentação
        isMoving = direction.magnitude;

        //Caso exista um controlador de animação
        if(animator != null)
            //Animations
            animator.SetFloat("isMoving", isMoving);
    }
}

//Enumerador para definir o tipo de Input que será utilizado
public enum InputType
{
    MOBILE,
    PC
}
