using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levelwise_Viewpoint_Creater.Model
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class exchange
    {

        private exchangeViewpoints viewpointsField;

        private string unitsField;

        private string filenameField;

        private string filepathField;

        /// <remarks/>
        public exchangeViewpoints viewpoints
        {
            get
            {
                return this.viewpointsField;
            }
            set
            {
                this.viewpointsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string units
        {
            get
            {
                return this.unitsField;
            }
            set
            {
                this.unitsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string filename
        {
            get
            {
                return this.filenameField;
            }
            set
            {
                this.filenameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string filepath
        {
            get
            {
                return this.filepathField;
            }
            set
            {
                this.filepathField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpoints
    {

        private exchangeViewpointsViewfolder[] viewfolderField;

        private exchangeViewpointsView[] viewField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("viewfolder")]
        public exchangeViewpointsViewfolder[] viewfolder
        {
            get
            {
                return this.viewfolderField;
            }
            set
            {
                this.viewfolderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("view")]
        public exchangeViewpointsView[] view
        {
            get
            {
                return this.viewField;
            }
            set
            {
                this.viewField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolder
    {

        private exchangeViewpointsViewfolderView[] viewField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("view")]
        public exchangeViewpointsViewfolderView[] view
        {
            get
            {
                return this.viewField;
            }
            set
            {
                this.viewField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderView
    {

        private exchangeViewpointsViewfolderViewViewpoint viewpointField;

        private exchangeViewpointsViewfolderViewClipplaneset clipplanesetField;

        private string nameField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewViewpoint viewpoint
        {
            get
            {
                return this.viewpointField;
            }
            set
            {
                this.viewpointField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplaneset clipplaneset
        {
            get
            {
                return this.clipplanesetField;
            }
            set
            {
                this.clipplanesetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewViewpoint
    {

        private exchangeViewpointsViewfolderViewViewpointCamera cameraField;

        private exchangeViewpointsViewfolderViewViewpointUP upField;

        private string renderField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewViewpointCamera camera
        {
            get
            {
                return this.cameraField;
            }
            set
            {
                this.cameraField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewfolderViewViewpointUP up
        {
            get
            {
                return this.upField;
            }
            set
            {
                this.upField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string render
        {
            get
            {
                return this.renderField;
            }
            set
            {
                this.renderField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewViewpointCamera
    {

        private exchangeViewpointsViewfolderViewViewpointCameraPosition positionField;

        private exchangeViewpointsViewfolderViewViewpointCameraRotation rotationField;

        private string projectionField;

        private decimal nearField;

        private decimal farField;

        private decimal aspectField;

        private decimal heightField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewViewpointCameraPosition position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewfolderViewViewpointCameraRotation rotation
        {
            get
            {
                return this.rotationField;
            }
            set
            {
                this.rotationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string projection
        {
            get
            {
                return this.projectionField;
            }
            set
            {
                this.projectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal near
        {
            get
            {
                return this.nearField;
            }
            set
            {
                this.nearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal far
        {
            get
            {
                return this.farField;
            }
            set
            {
                this.farField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal aspect
        {
            get
            {
                return this.aspectField;
            }
            set
            {
                this.aspectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewViewpointCameraPosition
    {

        private exchangeViewpointsViewfolderViewViewpointCameraPositionPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewViewpointCameraPositionPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewViewpointCameraPositionPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewViewpointCameraRotation
    {

        private exchangeViewpointsViewfolderViewViewpointCameraRotationQuaternion quaternionField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewViewpointCameraRotationQuaternion quaternion
        {
            get
            {
                return this.quaternionField;
            }
            set
            {
                this.quaternionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewViewpointCameraRotationQuaternion
    {

        private decimal aField;

        private decimal bField;

        private decimal cField;

        private decimal dField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal a
        {
            get
            {
                return this.aField;
            }
            set
            {
                this.aField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal b
        {
            get
            {
                return this.bField;
            }
            set
            {
                this.bField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal c
        {
            get
            {
                return this.cField;
            }
            set
            {
                this.cField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal d
        {
            get
            {
                return this.dField;
            }
            set
            {
                this.dField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewViewpointUP
    {

        private exchangeViewpointsViewfolderViewViewpointUPVec3f vec3fField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewViewpointUPVec3f vec3f
        {
            get
            {
                return this.vec3fField;
            }
            set
            {
                this.vec3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewViewpointUPVec3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplaneset
    {

        private exchangeViewpointsViewfolderViewClipplanesetRange rangeField;

        private exchangeViewpointsViewfolderViewClipplanesetClipplane[] clipplanesField;

        private exchangeViewpointsViewfolderViewClipplanesetBox boxField;

        private exchangeViewpointsViewfolderViewClipplanesetBoxrotation boxrotationField;

        private byte linkedField;

        private byte currentField;

        private string modeField;

        private byte enabledField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetRange range
        {
            get
            {
                return this.rangeField;
            }
            set
            {
                this.rangeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("clipplane", IsNullable = false)]
        public exchangeViewpointsViewfolderViewClipplanesetClipplane[] clipplanes
        {
            get
            {
                return this.clipplanesField;
            }
            set
            {
                this.clipplanesField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetBox box
        {
            get
            {
                return this.boxField;
            }
            set
            {
                this.boxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("box-rotation")]
        public exchangeViewpointsViewfolderViewClipplanesetBoxrotation boxrotation
        {
            get
            {
                return this.boxrotationField;
            }
            set
            {
                this.boxrotationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte linked
        {
            get
            {
                return this.linkedField;
            }
            set
            {
                this.linkedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte current
        {
            get
            {
                return this.currentField;
            }
            set
            {
                this.currentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string mode
        {
            get
            {
                return this.modeField;
            }
            set
            {
                this.modeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetRange
    {

        private exchangeViewpointsViewfolderViewClipplanesetRangeBox3f box3fField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetRangeBox3f box3f
        {
            get
            {
                return this.box3fField;
            }
            set
            {
                this.box3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetRangeBox3f
    {

        private exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMin minField;

        private exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMax maxField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMin min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMax max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMin
    {

        private exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMinPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMinPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMinPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMax
    {

        private exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMaxPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMaxPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetRangeBox3fMaxPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetClipplane
    {

        private exchangeViewpointsViewfolderViewClipplanesetClipplanePlane planeField;

        private string stateField;

        private decimal distanceField;

        private string alignmentField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetClipplanePlane plane
        {
            get
            {
                return this.planeField;
            }
            set
            {
                this.planeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal distance
        {
            get
            {
                return this.distanceField;
            }
            set
            {
                this.distanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string alignment
        {
            get
            {
                return this.alignmentField;
            }
            set
            {
                this.alignmentField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetClipplanePlane
    {

        private exchangeViewpointsViewfolderViewClipplanesetClipplanePlaneVec3f vec3fField;

        private decimal distanceField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetClipplanePlaneVec3f vec3f
        {
            get
            {
                return this.vec3fField;
            }
            set
            {
                this.vec3fField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal distance
        {
            get
            {
                return this.distanceField;
            }
            set
            {
                this.distanceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetClipplanePlaneVec3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBox
    {

        private exchangeViewpointsViewfolderViewClipplanesetBoxBox3f box3fField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetBoxBox3f box3f
        {
            get
            {
                return this.box3fField;
            }
            set
            {
                this.box3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBoxBox3f
    {

        private exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMin minField;

        private exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMax maxField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMin min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMax max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMin
    {

        private exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMinPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMinPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMinPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMax
    {

        private exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMaxPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMaxPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBoxBox3fMaxPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBoxrotation
    {

        private exchangeViewpointsViewfolderViewClipplanesetBoxrotationRotation rotationField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetBoxrotationRotation rotation
        {
            get
            {
                return this.rotationField;
            }
            set
            {
                this.rotationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBoxrotationRotation
    {

        private exchangeViewpointsViewfolderViewClipplanesetBoxrotationRotationQuaternion quaternionField;

        /// <remarks/>
        public exchangeViewpointsViewfolderViewClipplanesetBoxrotationRotationQuaternion quaternion
        {
            get
            {
                return this.quaternionField;
            }
            set
            {
                this.quaternionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewfolderViewClipplanesetBoxrotationRotationQuaternion
    {

        private decimal aField;

        private decimal bField;

        private decimal cField;

        private decimal dField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal a
        {
            get
            {
                return this.aField;
            }
            set
            {
                this.aField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal b
        {
            get
            {
                return this.bField;
            }
            set
            {
                this.bField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal c
        {
            get
            {
                return this.cField;
            }
            set
            {
                this.cField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal d
        {
            get
            {
                return this.dField;
            }
            set
            {
                this.dField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsView
    {

        private exchangeViewpointsViewViewpoint viewpointField;

        private exchangeViewpointsViewClipplaneset clipplanesetField;

        private string nameField;

        /// <remarks/>
        public exchangeViewpointsViewViewpoint viewpoint
        {
            get
            {
                return this.viewpointField;
            }
            set
            {
                this.viewpointField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewClipplaneset clipplaneset
        {
            get
            {
                return this.clipplanesetField;
            }
            set
            {
                this.clipplanesetField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpoint
    {

        private exchangeViewpointsViewViewpointCamera cameraField;

        private exchangeViewpointsViewViewpointViewer viewerField;

        private exchangeViewpointsViewViewpointUP upField;

        private string toolField;

        private string renderField;

        private string lightingField;

        private decimal focalField;

        /// <remarks/>
        public exchangeViewpointsViewViewpointCamera camera
        {
            get
            {
                return this.cameraField;
            }
            set
            {
                this.cameraField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewViewpointViewer viewer
        {
            get
            {
                return this.viewerField;
            }
            set
            {
                this.viewerField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewViewpointUP up
        {
            get
            {
                return this.upField;
            }
            set
            {
                this.upField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string tool
        {
            get
            {
                return this.toolField;
            }
            set
            {
                this.toolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string render
        {
            get
            {
                return this.renderField;
            }
            set
            {
                this.renderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lighting
        {
            get
            {
                return this.lightingField;
            }
            set
            {
                this.lightingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal focal
        {
            get
            {
                return this.focalField;
            }
            set
            {
                this.focalField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpointCamera
    {

        private exchangeViewpointsViewViewpointCameraPosition positionField;

        private exchangeViewpointsViewViewpointCameraRotation rotationField;

        private string projectionField;

        private decimal nearField;

        private decimal farField;

        private decimal aspectField;

        private decimal heightField;

        /// <remarks/>
        public exchangeViewpointsViewViewpointCameraPosition position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewViewpointCameraRotation rotation
        {
            get
            {
                return this.rotationField;
            }
            set
            {
                this.rotationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string projection
        {
            get
            {
                return this.projectionField;
            }
            set
            {
                this.projectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal near
        {
            get
            {
                return this.nearField;
            }
            set
            {
                this.nearField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal far
        {
            get
            {
                return this.farField;
            }
            set
            {
                this.farField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal aspect
        {
            get
            {
                return this.aspectField;
            }
            set
            {
                this.aspectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpointCameraPosition
    {

        private exchangeViewpointsViewViewpointCameraPositionPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewViewpointCameraPositionPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpointCameraPositionPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpointCameraRotation
    {

        private exchangeViewpointsViewViewpointCameraRotationQuaternion quaternionField;

        /// <remarks/>
        public exchangeViewpointsViewViewpointCameraRotationQuaternion quaternion
        {
            get
            {
                return this.quaternionField;
            }
            set
            {
                this.quaternionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpointCameraRotationQuaternion
    {

        private decimal aField;

        private decimal bField;

        private decimal cField;

        private decimal dField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal a
        {
            get
            {
                return this.aField;
            }
            set
            {
                this.aField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal b
        {
            get
            {
                return this.bField;
            }
            set
            {
                this.bField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal c
        {
            get
            {
                return this.cField;
            }
            set
            {
                this.cField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal d
        {
            get
            {
                return this.dField;
            }
            set
            {
                this.dField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpointViewer
    {

        private decimal radiusField;

        private decimal heightField;

        private decimal actual_heightField;

        private decimal eye_heightField;

        private string avatarField;

        private string camera_modeField;

        private decimal first_to_third_angleField;

        private decimal first_to_third_distanceField;

        private decimal first_to_third_paramField;

        private byte first_to_third_correctionField;

        private byte collision_detectionField;

        private byte auto_crouchField;

        private byte gravityField;

        private decimal gravity_valueField;

        private decimal terminal_velocityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal radius
        {
            get
            {
                return this.radiusField;
            }
            set
            {
                this.radiusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal actual_height
        {
            get
            {
                return this.actual_heightField;
            }
            set
            {
                this.actual_heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal eye_height
        {
            get
            {
                return this.eye_heightField;
            }
            set
            {
                this.eye_heightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string avatar
        {
            get
            {
                return this.avatarField;
            }
            set
            {
                this.avatarField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string camera_mode
        {
            get
            {
                return this.camera_modeField;
            }
            set
            {
                this.camera_modeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal first_to_third_angle
        {
            get
            {
                return this.first_to_third_angleField;
            }
            set
            {
                this.first_to_third_angleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal first_to_third_distance
        {
            get
            {
                return this.first_to_third_distanceField;
            }
            set
            {
                this.first_to_third_distanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal first_to_third_param
        {
            get
            {
                return this.first_to_third_paramField;
            }
            set
            {
                this.first_to_third_paramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte first_to_third_correction
        {
            get
            {
                return this.first_to_third_correctionField;
            }
            set
            {
                this.first_to_third_correctionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte collision_detection
        {
            get
            {
                return this.collision_detectionField;
            }
            set
            {
                this.collision_detectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte auto_crouch
        {
            get
            {
                return this.auto_crouchField;
            }
            set
            {
                this.auto_crouchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte gravity
        {
            get
            {
                return this.gravityField;
            }
            set
            {
                this.gravityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal gravity_value
        {
            get
            {
                return this.gravity_valueField;
            }
            set
            {
                this.gravity_valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal terminal_velocity
        {
            get
            {
                return this.terminal_velocityField;
            }
            set
            {
                this.terminal_velocityField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpointUP
    {

        private exchangeViewpointsViewViewpointUPVec3f vec3fField;

        /// <remarks/>
        public exchangeViewpointsViewViewpointUPVec3f vec3f
        {
            get
            {
                return this.vec3fField;
            }
            set
            {
                this.vec3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewViewpointUPVec3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplaneset
    {

        private exchangeViewpointsViewClipplanesetRange rangeField;

        private exchangeViewpointsViewClipplanesetClipplane[] clipplanesField;

        private exchangeViewpointsViewClipplanesetBox boxField;

        private exchangeViewpointsViewClipplanesetBoxrotation boxrotationField;

        private byte linkedField;

        private byte currentField;

        private string modeField;

        private byte enabledField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetRange range
        {
            get
            {
                return this.rangeField;
            }
            set
            {
                this.rangeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("clipplane", IsNullable = false)]
        public exchangeViewpointsViewClipplanesetClipplane[] clipplanes
        {
            get
            {
                return this.clipplanesField;
            }
            set
            {
                this.clipplanesField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetBox box
        {
            get
            {
                return this.boxField;
            }
            set
            {
                this.boxField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("box-rotation")]
        public exchangeViewpointsViewClipplanesetBoxrotation boxrotation
        {
            get
            {
                return this.boxrotationField;
            }
            set
            {
                this.boxrotationField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte linked
        {
            get
            {
                return this.linkedField;
            }
            set
            {
                this.linkedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte current
        {
            get
            {
                return this.currentField;
            }
            set
            {
                this.currentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string mode
        {
            get
            {
                return this.modeField;
            }
            set
            {
                this.modeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte enabled
        {
            get
            {
                return this.enabledField;
            }
            set
            {
                this.enabledField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetRange
    {

        private exchangeViewpointsViewClipplanesetRangeBox3f box3fField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetRangeBox3f box3f
        {
            get
            {
                return this.box3fField;
            }
            set
            {
                this.box3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetRangeBox3f
    {

        private exchangeViewpointsViewClipplanesetRangeBox3fMin minField;

        private exchangeViewpointsViewClipplanesetRangeBox3fMax maxField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetRangeBox3fMin min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetRangeBox3fMax max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetRangeBox3fMin
    {

        private exchangeViewpointsViewClipplanesetRangeBox3fMinPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetRangeBox3fMinPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetRangeBox3fMinPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetRangeBox3fMax
    {

        private exchangeViewpointsViewClipplanesetRangeBox3fMaxPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetRangeBox3fMaxPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetRangeBox3fMaxPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetClipplane
    {

        private exchangeViewpointsViewClipplanesetClipplanePlane planeField;

        private string stateField;

        private decimal distanceField;

        private string alignmentField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetClipplanePlane plane
        {
            get
            {
                return this.planeField;
            }
            set
            {
                this.planeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal distance
        {
            get
            {
                return this.distanceField;
            }
            set
            {
                this.distanceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string alignment
        {
            get
            {
                return this.alignmentField;
            }
            set
            {
                this.alignmentField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetClipplanePlane
    {

        private exchangeViewpointsViewClipplanesetClipplanePlaneVec3f vec3fField;

        private decimal distanceField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetClipplanePlaneVec3f vec3f
        {
            get
            {
                return this.vec3fField;
            }
            set
            {
                this.vec3fField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal distance
        {
            get
            {
                return this.distanceField;
            }
            set
            {
                this.distanceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetClipplanePlaneVec3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBox
    {

        private exchangeViewpointsViewClipplanesetBoxBox3f box3fField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetBoxBox3f box3f
        {
            get
            {
                return this.box3fField;
            }
            set
            {
                this.box3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBoxBox3f
    {

        private exchangeViewpointsViewClipplanesetBoxBox3fMin minField;

        private exchangeViewpointsViewClipplanesetBoxBox3fMax maxField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetBoxBox3fMin min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetBoxBox3fMax max
        {
            get
            {
                return this.maxField;
            }
            set
            {
                this.maxField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBoxBox3fMin
    {

        private exchangeViewpointsViewClipplanesetBoxBox3fMinPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetBoxBox3fMinPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBoxBox3fMinPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBoxBox3fMax
    {

        private exchangeViewpointsViewClipplanesetBoxBox3fMaxPos3f pos3fField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetBoxBox3fMaxPos3f pos3f
        {
            get
            {
                return this.pos3fField;
            }
            set
            {
                this.pos3fField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBoxBox3fMaxPos3f
    {

        private decimal xField;

        private decimal yField;

        private decimal zField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal x
        {
            get
            {
                return this.xField;
            }
            set
            {
                this.xField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal y
        {
            get
            {
                return this.yField;
            }
            set
            {
                this.yField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal z
        {
            get
            {
                return this.zField;
            }
            set
            {
                this.zField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBoxrotation
    {

        private exchangeViewpointsViewClipplanesetBoxrotationRotation rotationField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetBoxrotationRotation rotation
        {
            get
            {
                return this.rotationField;
            }
            set
            {
                this.rotationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBoxrotationRotation
    {

        private exchangeViewpointsViewClipplanesetBoxrotationRotationQuaternion quaternionField;

        /// <remarks/>
        public exchangeViewpointsViewClipplanesetBoxrotationRotationQuaternion quaternion
        {
            get
            {
                return this.quaternionField;
            }
            set
            {
                this.quaternionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class exchangeViewpointsViewClipplanesetBoxrotationRotationQuaternion
    {

        private decimal aField;

        private decimal bField;

        private decimal cField;

        private decimal dField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal a
        {
            get
            {
                return this.aField;
            }
            set
            {
                this.aField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal b
        {
            get
            {
                return this.bField;
            }
            set
            {
                this.bField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal c
        {
            get
            {
                return this.cField;
            }
            set
            {
                this.cField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal d
        {
            get
            {
                return this.dField;
            }
            set
            {
                this.dField = value;
            }
        }
    }




}
