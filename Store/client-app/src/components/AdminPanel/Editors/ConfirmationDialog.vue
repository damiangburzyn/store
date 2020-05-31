<template>



    <div class="text-center">
        <v-dialog v-model="Show"
                  width="400">
        
            <v-card>
                <v-card-title class="headline grey lighten-2"
                              primary-title>
                    {{ currentTitle }}
                </v-card-title>

                <v-card-text>
                    <br />
                {{Message}}
                </v-card-text>

                <v-divider></v-divider>

                <v-card-actions>
                    <v-btn color="primary"
                           text
                           @click="Cancel()">
                        Anuluj
                    </v-btn>
                    <v-spacer></v-spacer>
                    <v-btn color="primary"
                           text
                           @click="Confirm()">
                        Usuń
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </div>   
</template>


<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    @Component
    export default class ConfirmationDialog extends Vue {
        private msg!: string;
        @Prop(Boolean) readonly IsShow!: boolean;   
        @Prop(String) readonly Message!: string;    

        public Show = this.IsShow;
       
     
        Confirm() {

              this.$emit("Confirm");
        }

        Cancel() {
            this.$emit("Cancel");
        }

     

        // dialog ma możliwość lokalnej zmiany property
        @Watch('Show')
        onPropertyChanged(value: boolean, oldValue: boolean) {
            if (value == false) {
                this.closeDialog();
            }         
        }
       
        closeDialog() {
            this.$emit("dialog-closed");
        }

        ///gdy zmienia się property przez parenta
        @Watch('IsShow')
        onPropertyChangedtwo(value: boolean, oldValue: boolean) {
            this.Show = value;
        }

        get currentTitle() {
            return "Usuń"
        }
    }
</script>