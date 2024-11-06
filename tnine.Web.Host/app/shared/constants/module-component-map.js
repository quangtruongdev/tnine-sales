export const MODULE_PATHS = {
    APP_ADMIN_ROLE_MODULE: 'app/admin/authorization/role/role.module.js',
    APP_ADMIN_ROLE_COMPONENT: 'app/admin/authorization/role/role.component.js',
    APP_ADMIN_ROLE_CREATE_OR_EDIT_ROLE_MODAL: 'app/admin/authorization/role/create-or-edit-role-modal/create-or-edit-role-modal.component.js',
};

export const MODULE_COMPONENT_MAP = {
    'role': {
        module: MODULE_PATHS.APP_ADMIN_ROLE_MODULE,
        component: MODULE_PATHS.APP_ADMIN_ROLE_COMPONENT,
        createOrEditRoleModal: MODULE_PATHS.APP_ADMIN_ROLE_CREATE_OR_EDIT_ROLE_MODAL
    }
};