export const navigation = [
  {
    'id': 'chatGroup',
    'title': 'Chat',
    'translate': 'NAV.CHAT',
    'type': 'group',
    'hidden': false,
    'children': [      
        {
          'id': 'chat',
          'title': 'Chat',
        'translate': 'NAV.CHAT',
          'type': 'item',
        'icon': 'chat',
          'url': '/chat'
        }
    ]
  },
  {
    'id': 'admin',
    'title': 'Administrativo',
    'translate': 'NAV.ADMINISTRATIVO',
    'type': 'group',
    'hidden' : false,
    'children': [
      {
        'id': 'dashboards',
        'title': 'Dashboards',
        'translate': 'NAV.DASHBOARD',
        'type': 'item',
        'icon': 'dashboard',
        'url': '/administracao/dashboard'
      },
      {
        'id': 'clientes',
        'title': 'Clientes',
        'translate': 'NAV.CLIENTES',
        'type': 'item',
        'icon': 'android',
        'url': '/administracao/clientes'
      },
      {
        'id': 'dotes',
        'title': 'Dotes',
        'translate': 'NAV.DOTES',
        'type': 'item',
        'icon': 'fitness_center',
        'url': '/administracao/dotes'
      },
      {
        'id': 'planos',
        'title': 'Planos',
        'translate': 'NAV.PLANOS',
        'type': 'item',
        'icon': 'attach_money',
        'url': '/administracao/planos'
      },
      {
        'id': 'validacoes',
        'title': 'Validações',
        'translate': 'NAV.VALIDACOES',
        'type': 'collapse',
        'icon': 'assignment_turned_in',
        'children': [
          {
            'id': 'validarPerfil',
            'title': 'Perfil',
            'translate': 'NAV.VALIDA.PERFIL',
            'type': 'item',
            'url': '/administracao/validar/perfil'
           
          },
          {
            'id': 'validarFoto',
            'title': 'Foto',
            'translate': 'NAV.VALIDA.FOTO',
            'type': 'item',
            'url': '/administracao/validar/foto'
            
          },
          {
            'id': 'validarVideo',
            'title': 'Vídeo',
            'translate': 'NAV.VALIDA.VIDEO',
            'type': 'item',
            'url': '/administracao/validar/video'
          }
        ]
      },      
      {
        'id': 'assinaturas',
        'title': 'Assinaturas',
        'translate': 'NAV.ASSINATURAS',
        'type': 'item',
        'icon': 'description',
        'url': '/administracao/assinaturas'
      },
      {
        'id': 'contatos',
        'title': 'Contatos',
        'translate': 'NAV.CONTATOS',
        'type': 'item',
        'icon': 'contact_mail',
        'url': '/administracao/contatos'
      },

      ]
  },
   {
    'id': 'desfrutar',
    'title': 'Desfrutar',
    'translate': 'NAV.DESFRUTAR',
    'type': 'group',
     'hidden': false,
     'children': [
       {
         'id': 'quero_desfrutar',
         'title': 'Quero desfrutar',
         'translate': 'NAV.QUERO_DESFRUTAR',
         'type': 'item',
         'icon': 'location_on',
         'url': '/quero-desfrutar'
       },
       {
         'id': 'desfrutarAssinatura',
         'title': 'Assinaturas',
         'translate': 'NAV.ASSINATURAS',
         'type': 'item',
         'icon': 'dashboard',
         'url': '/assinaturas'
       }
     ]
  },
  {
    'id': 'oferecer',
    'title': 'oferecer',
    'translate': 'NAV.OFERECER',
    'type': 'group',
    'hidden': false,
    'children': [
      {
        'id': 'perfil',
        'title': 'Perfil',
        'translate': 'NAV.PERFIL',
        'type': 'item',
        'icon': 'contacts',
        'url': '/perfil'
      },
      {
        'id': 'agenda',
        'title': 'Agenda',
        'translate': 'NAV.AGENDA',
        'type': 'item',
        'icon': 'today',
        'url': '/agenda'
      }
      ]
  }
];
