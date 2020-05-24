<template>
    <div>
        kategorie 
        <CategoryEditor/>
</div>
   
</template>


<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import CategoryEditor from "@/components/AdminPanel/Editors/CategoryEditor.vue"
    @Component({
        components: {
            CategoryEditor: CategoryEditor
        },
    })
    export default class Categories extends Vue { }

</script>