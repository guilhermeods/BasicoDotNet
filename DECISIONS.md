# Arquivo de Decisoes Tecnicas

Este documento registra decisoes arquiteturais e tecnicas importantes
tomadas durante o desenvolvimento da API de Avisos (v1 e v2).
------------------------------------------------------------------------

## 1. Versionamento da API (v1 → v2)
Implementar **versionamento por URL** no formato:

    api/v{version}/controller

### Motivo

-   Permitir evolucao sem quebrar integracoes existentes.

### Impacto

-   A v1 continua funcionando.
-   A v2 recebe novas propriedades e endpoints evoluidos.

------------------------------------------------------------------------

## 2. Evolução da Entidade `AvisoEntity`

### Decisão

Expandir a entidade: - `Mensagem` - `CriadoEm` - `AtualizadoEm` -
`Excluido` (para soft delete)

### Motivo

Manter historico, controlar atualizacao e permitir futura auditoria.

### Impacto

-   A v1 continua funcionando pois so utiliza os campos antigos.
-   A v2 passa a retornar todas as informacoes do aviso.

------------------------------------------------------------------------

## 3. Soft Delete (campo `Excluido`)

### Decisão

- Nao remover o registro fisicamente.
- Usar o campo `Excluido = true` como exclusao logica.
- Nao usar o campo Ativo, pois em futura auditoria pode usar para ver campos excluidos apenas.
- Ativado pode ficar temporariamente e depois voltar.

### Motivo

-   Evitar perda de dados.
-   Facilitar auditoria futura.
-   Garantir integridade referencial.

###  Impacto

-   Endpoints v2 so retornam registros ativos.
-   Delete passa a ser não-destrutivo.

------------------------------------------------------------------------

## 4. Nome dos campos misto (PT + EN)

### Decisao

Manter padrao legado PT-BR para o dominio, mas permitir campos
adicionais em EN na v2.

#### Exemplo:

-   `Ativo` 
-   `CriadoEm`
-   `AtualizadoEm`
-   `Excluido` 

### Motivo

Preservar compatibilidade com o banco + manter clareza interna.

------------------------------------------------------------------------

## 5. Uso de UTC para datas

### Decisao

Sempre usar `DateTime.UtcNow` em criacao/edicao.

### Motivo

-   Evita problemas de fuso horario.
-   Garante consistencia para multiplas regioes.

------------------------------------------------------------------------
